﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.EntityFrameworkCore;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.Exceptions;
using SimpleIdServer.IdServer.Store;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.Helpers
{
    public interface IGrantHelper
    {
        Task<GrantRequest> Extract(IEnumerable<string> scopes, IEnumerable<string> resources, CancellationToken cancellationToken);
    }

    public class GrantRequest
    {
        public GrantRequest(ICollection<AuthorizedScope> authorizations)
        {
            Authorizations = authorizations;
        }

        public IEnumerable<string> Scopes
        {
            get
            {
                return Authorizations.Select(a => a.Scope).Distinct();
            }
        }

        public IEnumerable<string> Audiences
        {
            get
            {
                return Authorizations.SelectMany(a => a.Resources).Distinct();
            }
        }

        public ICollection<AuthorizedScope> Authorizations { get; private set; }
    }

    public class GrantHelper : IGrantHelper
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IClientRepository _clientRepository;

        public GrantHelper(IApiResourceRepository apiResourceRepository, IClientRepository clientRepository)
        {
            _apiResourceRepository = apiResourceRepository;
            _clientRepository = clientRepository;
        }

        public async Task<GrantRequest> Extract(IEnumerable<string> scopes, IEnumerable<string> resources, CancellationToken cancellationToken)
        {
            var authResults = new List<AuthorizedScope>();
            // TODO : refactor this part???
            if (resources.Any())
                authResults.AddRange(await ProcessResourceParameter(resources, scopes, cancellationToken));

            var unknownScopes = scopes.Where(s => !authResults.Any(a => a.Scope == s));
            authResults.AddRange(await ProcessScopeParameter(scopes, cancellationToken));
            return new GrantRequest(authResults);

            async Task<List<AuthorizedScope>> ProcessResourceParameter(IEnumerable<string> resources, IEnumerable<string> scopes, CancellationToken cancellationToken)
            {
                var authResults = new List<AuthorizedScope>();
                var apiResources = await _apiResourceRepository.Query().Include(r => r.Scopes).Where(r => resources.Contains(r.Name)).ToListAsync(cancellationToken);
                var unsupportedResources = resources.Where(r => !apiResources.Any(a => a.Name == r));
                if (unsupportedResources.Any())
                    throw new OAuthException(ErrorCodes.INVALID_TARGET, string.Format(ErrorMessages.UKNOWN_RESOURCE, string.Join(",", unsupportedResources)));
                var allApiResourceScopes = apiResources.SelectMany(c => c.Scopes).GroupBy(s => s.Name).Select(k => k.Key);
                var supportedScopes = scopes.Where(s => apiResources.Any(r => r.Scopes.Any(sc => sc.Name == s)));
                if (!supportedScopes.Any())
                    supportedScopes = allApiResourceScopes;
                var result = new List<AuthorizedScope>();
                foreach(var scope in supportedScopes)
                    result.Add(new AuthorizedScope
                    {
                        Scope = scope,
                        Resources = apiResources.Where(r => r.Scopes.Any(s => s.Name == scope)).Select(r => r.Name).ToList()
                    });

                return result;
            }

            async Task<List<AuthorizedScope>> ProcessScopeParameter(IEnumerable<string> scopes, CancellationToken cancellationToken)
            {
                var apiResources = await _apiResourceRepository.Query().Include(r => r.Scopes).Where(r => r.Scopes.Any(s => scopes.Contains(s.Name))).ToListAsync(cancellationToken);
                var result = new List<AuthorizedScope>();
                foreach (var scope in scopes)
                    result.Add(new AuthorizedScope
                    {
                        Scope = scope,
                        Resources = apiResources.Where(r => r.Scopes.Any(s => s.Name == scope)).Select(r => r.Name).ToList()
                    });

                return result;
            }
        }
    }
}