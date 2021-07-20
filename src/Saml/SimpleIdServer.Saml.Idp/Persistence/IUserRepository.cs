﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using SimpleIdServer.Common.Domains;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.Saml.Idp.Persistence
{
    public interface IUserRepository
    {
        Task<User> Get(string id, CancellationToken cancellationToken);
    }
}