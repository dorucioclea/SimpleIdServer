﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SimpleIdServer.IdServer.Api;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.Helpers;
using SimpleIdServer.IdServer.Options;
using SimpleIdServer.IdServer.Store;
using SimpleIdServer.IdServer.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.UI;

[Area(Constants.Areas.Password)]
public class RegisterController : BaseRegisterController<PwdRegisterViewModel>
{
    public RegisterController(IOptions<IdServerHostOptions> options, IDistributedCache distributedCache, IUserRepository userRepository, IUserClaimsService userClaimsService) : base(options, distributedCache, userRepository, userClaimsService)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromRoute] string prefix)
    {
        prefix = prefix ?? Constants.Prefix;
        var viewModel = new PwdRegisterViewModel();
        var isAuthenticated = User.Identity.IsAuthenticated;
        var userRegistrationProgress = await GetRegistrationProgress();
        if (userRegistrationProgress == null && !isAuthenticated)
        {
            viewModel.IsNotAllowed = true;
            return View(viewModel);
        }

        if(isAuthenticated)
        {
            var nameIdentifier = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            viewModel.Login = nameIdentifier;
        }

        viewModel.Amr = userRegistrationProgress?.Amr;
        viewModel.Steps = userRegistrationProgress?.Steps;
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([FromRoute] string prefix, PwdRegisterViewModel viewModel)
    {
        prefix = prefix ?? Constants.Prefix;
        var isAuthenticated = User.Identity.IsAuthenticated;
        if (isAuthenticated)
        {
            var nameIdentifier = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            viewModel.Login = nameIdentifier;
        }

        var userRegistrationProgress = await GetRegistrationProgress();
        if (userRegistrationProgress == null && !isAuthenticated)
        {
            viewModel.IsNotAllowed = true;
            return View(viewModel);
        }

        viewModel.Amr = userRegistrationProgress?.Amr;
        viewModel.Steps = userRegistrationProgress?.Steps;
        viewModel.Validate(ModelState);
        if (!ModelState.IsValid) return View(viewModel);
        if (!isAuthenticated) return await CreateUser();
        return await UpdateUser();

        async Task<IActionResult> CreateUser()
        {
            var userExists = await UserRepository.Query().Include(u => u.Realms).AsNoTracking().AnyAsync(u => u.Name == viewModel.Login && u.Realms.Any(r => r.RealmsName == prefix));
            if(userExists)
            {
                ModelState.AddModelError("user_exists", "user_exists");
                return View(viewModel);
            }

            return await base.CreateUser(userRegistrationProgress, viewModel, prefix, Constants.Areas.Password);
        }

        async Task<IActionResult> UpdateUser()
        {
            var user = await UserRepository.Query().Include(u => u.Credentials).SingleAsync(u => u.Name == viewModel.Login);
            var passwordCredential = user.Credentials.FirstOrDefault(c => c.CredentialType == UserCredential.PWD);
            if (passwordCredential != null) passwordCredential.Value = PasswordHelper.ComputeHash(viewModel.Password);
            else user.Credentials.Add(new UserCredential
            {
                Id = Guid.NewGuid().ToString(),
                Value = PasswordHelper.ComputeHash(viewModel.Password),
                CredentialType = UserCredential.PWD,
                IsActive = true
            });
            await UserRepository.SaveChanges(CancellationToken.None);
            return await base.UpdateUser(userRegistrationProgress, viewModel, Constants.Areas.Password);
        }
    }

    protected override void EnrichUser(User user, ICollection<UserClaim> userClaims, PwdRegisterViewModel viewModel)
    {
        user.Credentials.Add(new UserCredential
        {
            Id = Guid.NewGuid().ToString(),
            CredentialType = "pwd",
            IsActive = true,
            Value = PasswordHelper.ComputeHash(viewModel.Password)
        });
        user.Name = viewModel.Login;
        if (Options.IsEmailUsedDuringAuthentication) user.Email = viewModel.Login;
    }
}