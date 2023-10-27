﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.Provisioning.LDAP.Jobs;
using SimpleIdServer.IdServer.UI.Services;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Claims;

namespace SimpleIdServer.IdServer.Provisioning.LDAP
{
    public class LDAPAuthenticationService : IIdProviderAuthService
    {
        private readonly ILogger<LDAPAuthenticationService> _logger;
        private readonly IConfiguration _configuration;

        public LDAPAuthenticationService(ILogger<LDAPAuthenticationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string Name => LDAPRepresentationsExtractionJob.NAME;

        public bool Authenticate(User user, ICollection<Claim> claims, IdentityProvisioning identityProvisioning, string password)
        {
            var distinguishedName = claims.Single(c => c.Type == Constants.LDAPDistinguishedName).Value;
            var section = _configuration.GetSection($"{identityProvisioning.Name}:{typeof(LDAPRepresentationsExtractionJobOptions).Name}");
            var options = section.Get<LDAPRepresentationsExtractionJobOptions>();
            var credentials = new NetworkCredential(distinguishedName, password);
            using(var ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(options.Server, options.Port), credentials, AuthType.Basic))
            {
                try
                {
                    ldapConnection.SessionOptions.ProtocolVersion = 3;
                    ldapConnection.Bind();
                    return true;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    return false;
                }
            }
        }
    }
}
