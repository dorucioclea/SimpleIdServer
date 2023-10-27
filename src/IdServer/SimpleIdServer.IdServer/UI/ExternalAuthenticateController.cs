﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using SimpleIdServer.IdServer.Api;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.Exceptions;
using SimpleIdServer.IdServer.Helpers;
using SimpleIdServer.IdServer.Options;
using SimpleIdServer.IdServer.Store;
using SimpleIdServer.IdServer.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.UI
{
    public class ExternalAuthenticateController : BaseAuthenticateController
    {
        public const string SCHEME_NAME = "scheme";
        public const string RETURN_URL_NAME = "returnUrl";
        private readonly ILogger<ExternalAuthenticateController> _logger;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        private readonly IUserTransformer _userTransformer;
        private readonly IAuthenticationSchemeProviderRepository _authenticationSchemeProviderRepository;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IRealmRepository _realmRepository;
        private readonly IUserClaimsService _userClaimsService;

        public ExternalAuthenticateController(
            IOptions<IdServerHostOptions> options,
            IDataProtectionProvider dataProtectionProvider,
            IClientRepository clientRepository,
            IAmrHelper amrHelper,
            IUserRepository userRepository,
            ILogger<ExternalAuthenticateController> logger,
            IAuthenticationSchemeProvider authenticationSchemeProvider,
            IUserTransformer userTransformer,
            IAuthenticationSchemeProviderRepository authenticationSchemeProviderRepository,
            IAuthenticationHelper authenticationHelper,
            IRealmRepository realmRepository,
            IUserClaimsService userClaimsService,
            IBusControl busControl) : base(clientRepository, userRepository, amrHelper, busControl, userTransformer, dataProtectionProvider, authenticationHelper, options)
        {
            _logger = logger;
            _authenticationSchemeProvider = authenticationSchemeProvider;
            _userTransformer = userTransformer;
            _authenticationSchemeProviderRepository = authenticationSchemeProviderRepository;
            _authenticationHelper = authenticationHelper;
            _realmRepository = realmRepository;
            _userClaimsService = userClaimsService;
        }

        [HttpGet]
        public IActionResult Login(string scheme, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(scheme))
                throw new OAuthException(ErrorCodes.INVALID_REQUEST, string.Format(ErrorMessages.MISSING_PARAMETER, nameof(scheme)));

            var items = new Dictionary<string, string>
            {
                { SCHEME_NAME, scheme }
            };
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                items.Add(RETURN_URL_NAME, returnUrl);
            }
            var props = new AuthenticationProperties(items)
            {
                RedirectUri = Url.Action(nameof(Callback)),
            };
            return Challenge(props, scheme);
        }

        [HttpGet]
        public async Task<IActionResult> Callback([FromRoute] string prefix, CancellationToken cancellationToken)
        {
            prefix = prefix ?? Constants.DefaultRealm;
            var result = await HttpContext.AuthenticateAsync(Constants.DefaultExternalCookieAuthenticationScheme);
            if (result == null || !result.Succeeded)
            {
                if (result.Failure != null)
                {
                    _logger.LogError(result.Failure.ToString());
                }

                throw new OAuthException(ErrorCodes.INVALID_REQUEST, ErrorMessages.BAD_EXTERNAL_AUTHENTICATION);
            }

            var user = await JustInTimeProvision(prefix, result, cancellationToken);
            await HttpContext.SignOutAsync(Constants.DefaultExternalCookieAuthenticationScheme);
            if (result.Properties.Items.ContainsKey(RETURN_URL_NAME))
                return await Authenticate(prefix, result.Properties.Items[RETURN_URL_NAME], Constants.Areas.Password, user, cancellationToken, false);     

            return await Sign(prefix, "~/", Constants.Areas.Password, user, cancellationToken, false);
        }

        private async Task<User> JustInTimeProvision(string realm, AuthenticateResult authResult, CancellationToken cancellationToken)
        {
            var scheme = authResult.Properties.Items[SCHEME_NAME];
            var principal = authResult.Principal;
            var sub = GetClaim(principal, JwtRegisteredClaimNames.Sub) ?? GetClaim(principal, ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(sub))
            {
                _logger.LogError("There is not valid subject");
                throw new OAuthException(ErrorCodes.INVALID_REQUEST, ErrorMessages.BAD_EXTERNAL_AUTHENTICATION_USER);
            }

            var userClaims = new List<UserClaim>();
            var user = await UserRepository.Query()
                .Include(u => u.Groups)
                .Include(u => u.ExternalAuthProviders)
                .Include(u => u.Sessions)
                .Include(u => u.Realms)
                .FirstOrDefaultAsync(u => u.ExternalAuthProviders.Any(e => e.Scheme == scheme && e.Subject == sub) && u.Realms.Any(r => r.RealmsName == realm), cancellationToken);
            if (user == null)
            {
                _logger.LogInformation($"Start to provision the user '{sub}'");
                var existingUser = await _authenticationHelper.GetUserByLogin(UserRepository.Query()
                    .Include(u => u.ExternalAuthProviders)
                    .Include(u => u.Sessions)
                    .Include(u => u.Realms)
                    .Include(u => u.Groups), sub, realm, cancellationToken);
                if(existingUser != null)
                {
                    user = existingUser;
                    user.AddExternalAuthProvider(scheme, sub);
                    await UserRepository.SaveChanges(cancellationToken);
                }
                else
                {
                    var r = await _realmRepository.Query().FirstAsync(r => r.Name == realm);
                    var idProvider = await _authenticationSchemeProviderRepository.Query().AsNoTracking().Include(p => p.Mappers).SingleAsync(p => p.Name == scheme, cancellationToken);
                    var transformationResult = _userTransformer.Transform(r, principal, idProvider);
                    userClaims = transformationResult.Claims;
                    user = transformationResult.User;
                    user.AddExternalAuthProvider(scheme, sub);
                    UserRepository.Add(user);
                    await UserRepository.SaveChanges(cancellationToken);
                }

                _logger.LogInformation($"Finish to provision the user '{sub}'");
            }

            return user;
        }

        public static string GetClaim(ClaimsPrincipal principal, string claimType)
        {
            var claim = principal.Claims.FirstOrDefault(c => c.Type == claimType);
            if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                return null;
            return claim.Value;
        }
    }
}