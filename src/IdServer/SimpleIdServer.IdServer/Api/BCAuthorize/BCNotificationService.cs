﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.Api.BCAuthorize
{
    public interface IBCNotificationService
    {
        Task Notify(HandlerContext handlerContext, BCNotificationMessage message, CancellationToken cancellationToken);
    }

    public class BCNotificationMessage
    {
        public string AuthReqId { get; set; }
        public string BindingMessage { get; set; }
        public string ClientId { get; set; }
        public IEnumerable<string> Scopes { get; set; } = new List<string>();
        public IEnumerable<string> AcrLst { get; set; } = new List<string>();
        public ICollection<AuthorizationData> AuthorizationDetails { get; set; } = new List<AuthorizationData>();
        public string Amr { get; set; }

        public List<KeyValuePair<string, string>> Serialize(UrlEncoder urlEncoder)
        {
            var result = new List<KeyValuePair<string, string>>();
            result.Add(new KeyValuePair<string, string>(BCAuthenticationResponseParameters.AuthReqId, AuthReqId));
            result.Add(new KeyValuePair<string, string>(AuthorizationRequestParameters.ClientId, ClientId));
            result.Add(new KeyValuePair<string, string>("amr", Amr));
            if (!string.IsNullOrWhiteSpace(BindingMessage))
                result.Add(new KeyValuePair<string, string>(BCAuthenticationRequestParameters.BindingMessage, BindingMessage));
            if (Scopes.Any())
                result.Add(new KeyValuePair<string, string>(AuthorizationRequestParameters.Scope, string.Join(" ", Scopes)));
            if (AcrLst.Any())
                result.Add(new KeyValuePair<string, string>(AuthorizationRequestParameters.AcrValue, string.Join(" ", AcrLst)));
            if (AuthorizationDetails.Any())
                result.Add(new KeyValuePair<string, string>(AuthorizationRequestParameters.AuthorizationDetails, urlEncoder.Encode(JsonSerializer.Serialize(AuthorizationDetails))));
            return result;
        }
    }

    public class BCNotificationService : IBCNotificationService
    {
        private readonly IDataProtector _dataProtector;
        private readonly UrlEncoder _urlEncoder;
        private readonly IEnumerable<IUserNotificationService> _notificationServices;

        public BCNotificationService(IDataProtectionProvider dataProtectionProvider, UrlEncoder urlEncoder, IEnumerable<IUserNotificationService> notificationServices)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("Authorization");
            _urlEncoder = urlEncoder;
            _notificationServices = notificationServices;
        }

        public async Task Notify(HandlerContext handlerContext, BCNotificationMessage message, CancellationToken cancellationToken)
        {
            var notificationMode = handlerContext.User.NotificationMode ?? Constants.DefaultNotificationMode;
            var notificationService = _notificationServices.First(n => n.Name == notificationMode);
            await notificationService.Send($"The Back Channel redirection URL is : {BuildUrl(handlerContext.UrlHelper, handlerContext.Request.IssuerName, message)}", handlerContext.User, handlerContext.UserClaims);
        }

        protected string BuildUrl(IUrlHelper urlHelper, string issuer, BCNotificationMessage message)
        {
            var queries = message.Serialize(_urlEncoder);
            var queryCollection = new QueryBuilder(queries);
            var returnUrl = $"{HandlerContext.GetIssuer(issuer)}/{Constants.EndPoints.BCCallback}{queryCollection.ToQueryString()}";
            return $"{issuer}{urlHelper.Action("Index", "BackChannelConsents", new
            {
                returnUrl = _dataProtector.Protect(returnUrl)
            })}";
        }
    }
}
