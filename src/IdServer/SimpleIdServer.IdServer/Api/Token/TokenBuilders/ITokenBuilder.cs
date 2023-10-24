﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using SimpleIdServer.IdServer.Domains;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.Api.Token.TokenBuilders
{
    public interface ITokenBuilder
    {
        string Name { get; }
        /// <summary>
        /// Build new token.
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="audiences"></param>
        /// <param name="handlerContext"></param>
        /// <returns></returns>
        Task Build(BuildTokenParameter parameter, HandlerContext handlerContext, CancellationToken cancellationToken, bool useOriginalRequest = false);
    }

    public class BuildTokenParameter
    {
        public ICollection<Claim> UserClaims { get; set; }
        public IEnumerable<string> Scopes { get; set; }
        public IEnumerable<string> Audiences { get; set; }
        public ICollection<AuthorizationData> AuthorizationDetails { get; set; }
        public IEnumerable<AuthorizedClaim> Claims { get; set; }
        public string GrantId { get; set; }
        public Dictionary<string, object> AdditionalClaims { get; set; }
    }
}