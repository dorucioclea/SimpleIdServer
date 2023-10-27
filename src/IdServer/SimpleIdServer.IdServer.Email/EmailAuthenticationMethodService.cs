﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using SimpleIdServer.IdServer.Domains;

namespace SimpleIdServer.IdServer.Email
{
    public class EmailAuthenticationMethodService : IAuthenticationMethodService
    {
        public string Amr => Constants.AMR;
        public string Name => "Email";
        public Type? OptionsType => typeof(IdServerEmailOptions);
        public bool IsCredentialExists(User user, List<UserClaim> claims) => !string.IsNullOrEmpty(user.Email);
    }
}