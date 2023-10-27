﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleIdServer.IdServer.Api.Token.Helpers;
using SimpleIdServer.IdServer.Api.Token.TokenBuilders;
using SimpleIdServer.IdServer.Api.Token.TokenProfiles;
using SimpleIdServer.IdServer.Api.Token.Validators;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.DTOs;
using SimpleIdServer.IdServer.Exceptions;
using SimpleIdServer.IdServer.ExternalEvents;
using SimpleIdServer.IdServer.Helpers;
using SimpleIdServer.IdServer.Options;
using SimpleIdServer.IdServer.Store;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.Api.Token.Handlers
{
    public class PasswordHandler : BaseCredentialsHandler
    {
        private readonly IPasswordGrantTypeValidator _passwordGrantTypeValidator;
        private readonly IUserRepository _userRepository;
        private readonly IEnumerable<ITokenBuilder> _tokenBuilders;
        private readonly IGrantHelper _audienceHelper;
        private readonly IAuthenticationHelper _userHelper;
        private readonly IBusControl _busControl;
        private readonly IDPOPProofValidator _dpopProofValidator;
        private readonly IUserClaimsService _userClaimsService;
        private readonly IdServerHostOptions _options;

        public PasswordHandler(
            IPasswordGrantTypeValidator passwordGrantTypeValidator,
            IUserRepository userRepository, 
            IEnumerable<ITokenProfile> tokenProfiles,
            IEnumerable<ITokenBuilder> tokenBuilders, 
            IClientAuthenticationHelper clientAuthenticationHelper,
            IGrantHelper audienceHelper,
            IAuthenticationHelper userHelper,
            IBusControl busControl,
            IDPOPProofValidator dpopProofValidator,
            IUserClaimsService userClaimsService,
            IOptions<IdServerHostOptions> options) : base(clientAuthenticationHelper, tokenProfiles, options)
        {
            _passwordGrantTypeValidator = passwordGrantTypeValidator;
            _userRepository = userRepository;
            _tokenBuilders = tokenBuilders;
            _audienceHelper = audienceHelper;
            _userHelper = userHelper;
            _busControl = busControl;
            _dpopProofValidator = dpopProofValidator;
            _userClaimsService = userClaimsService;
            _options = options.Value;
        }

        public const string GRANT_TYPE = "password";
        public override string GrantType => GRANT_TYPE;

        public override async Task<IActionResult> Handle(HandlerContext context, CancellationToken cancellationToken)
        {
            IEnumerable<string> scopeLst = new string[0];
            using (var activity = Tracing.IdServerActivitySource.StartActivity("Get Token"))
            {
                try
                {
                    activity?.SetTag("grant_type", GRANT_TYPE);
                    activity?.SetTag("realm", context.Realm);
                    _passwordGrantTypeValidator.Validate(context);
                    var oauthClient = await AuthenticateClient(context, cancellationToken);
                    context.SetClient(oauthClient);
                    activity?.SetTag("client_id", oauthClient.ClientId);
                    await _dpopProofValidator.Validate(context);
                    var scopes = ScopeHelper.Validate(context.Request.RequestData.GetStr(TokenRequestParameters.Scope), oauthClient.Scopes.Select(s => s.Name));
                    var resources = context.Request.RequestData.GetResourcesFromAuthorizationRequest();
                    var authDetails = context.Request.RequestData.GetAuthorizationDetailsFromAuthorizationRequest();
                    var extractionResult = await _audienceHelper.Extract(context.Realm ?? Constants.DefaultRealm, scopes, resources, new List<string>(), authDetails, cancellationToken);
                    scopeLst = extractionResult.Scopes;
                    activity?.SetTag("scopes", string.Join(",", extractionResult.Scopes)); 
                    var userName = context.Request.RequestData.GetStr(TokenRequestParameters.Username);
                    var password = context.Request.RequestData.GetStr(TokenRequestParameters.Password);
                    var user = await _userHelper.FilterUsersByLogin(_userRepository.Query()
                        .Include(u => u.Credentials)
                        .Include(u => u.Groups)
                        .Include(u => u.Realms)
                        .AsNoTracking(), userName, context.Realm).FirstOrDefaultAsync(u => u.Credentials.Any(c => c.CredentialType == UserCredential.PWD && c.Value == PasswordHelper.ComputeHash(password) && c.IsActive), cancellationToken);
                    if (user == null) return BuildError(HttpStatusCode.BadRequest, ErrorCodes.INVALID_GRANT, ErrorMessages.BAD_USER_CREDENTIAL);
                    var userClaims = await _userClaimsService.Get(user.Id, cancellationToken);
                    context.SetUser(user, userClaims);
                    var result = BuildResult(context, extractionResult.Scopes);
                    foreach (var tokenBuilder in _tokenBuilders)
                        await tokenBuilder.Build(new BuildTokenParameter { AuthorizationDetails = extractionResult.AuthorizationDetails, Scopes = extractionResult.Scopes, Audiences = extractionResult.Audiences }, context, cancellationToken);

                    AddTokenProfile(context);
                    foreach (var kvp in context.Response.Parameters)
                        result.Add(kvp.Key, kvp.Value);
                    await _busControl.Publish(new TokenIssuedSuccessEvent
                    {
                        GrantType = GRANT_TYPE,
                        ClientId = context.Client.ClientId,
                        Scopes = extractionResult.Scopes,
                        Realm = context.Realm
                    });
                    activity?.SetStatus(ActivityStatusCode.Ok, "Token has been issued");
                    return new OkObjectResult(result);
                }
                catch (OAuthUnauthorizedException ex)
                {
                    await _busControl.Publish(new TokenIssuedFailureEvent
                    {
                        GrantType = GRANT_TYPE,
                        ClientId = context.Client?.ClientId,
                        Scopes = scopeLst,
                        Realm = context.Realm,
                        ErrorMessage = ex.Message
                    });
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    return BuildError(HttpStatusCode.Unauthorized, ex.Code, ex.Message);
                }
                catch (OAuthException ex)
                {
                    await _busControl.Publish(new TokenIssuedFailureEvent
                    {
                        GrantType = GRANT_TYPE,
                        ClientId = context.Client?.ClientId,
                        Scopes = scopeLst,
                        Realm = context.Realm,
                        ErrorMessage = ex.Message
                    });
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    return BuildError(HttpStatusCode.BadRequest, ex.Code, ex.Message);
                }
            }
        }
    }
}