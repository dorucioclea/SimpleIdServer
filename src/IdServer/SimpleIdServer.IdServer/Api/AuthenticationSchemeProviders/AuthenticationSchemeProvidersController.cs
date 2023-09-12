﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleIdServer.IdServer.Domains;
using SimpleIdServer.IdServer.DTOs;
using SimpleIdServer.IdServer.Exceptions;
using SimpleIdServer.IdServer.Jwt;
using SimpleIdServer.IdServer.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.IdServer.Api.AuthenticationSchemeProviders;

public class AuthenticationSchemeProvidersController : BaseController
{
	private readonly IAuthenticationSchemeProviderRepository _authenticationSchemeProviderRepository;
	private readonly IAuthenticationSchemeProviderDefinitionRepository _authenticationSchemeProviderDefinitionRepository;
	private readonly IRealmRepository _realmRepository;
	private readonly IJwtBuilder _jwtBuilder;
	private readonly IConfiguration _configuration;

	public AuthenticationSchemeProvidersController(IAuthenticationSchemeProviderRepository authenticationSchemeProviderRepository, IAuthenticationSchemeProviderDefinitionRepository authenticationSchemeProviderDefinitionRepository, IRealmRepository realmRepository, IJwtBuilder jwtBuilder, IConfiguration configuration)
	{
		_authenticationSchemeProviderRepository = authenticationSchemeProviderRepository;
		_authenticationSchemeProviderDefinitionRepository = authenticationSchemeProviderDefinitionRepository;
		_realmRepository = realmRepository;
		_jwtBuilder = jwtBuilder;
		_configuration = configuration;
	}

	[HttpPost]
	public async Task<IActionResult> Search([FromRoute] string prefix, [FromBody] SearchRequest request)
	{
		prefix = prefix ?? Constants.DefaultRealm;
		try
		{
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
            IQueryable<AuthenticationSchemeProvider> query = _authenticationSchemeProviderRepository.Query()
				.Include(p => p.Realms)
				.Where(p => p.Realms.Any(r => r.Name == prefix))
				.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Filter))
                query = query.Where(request.Filter);

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
				query = query.OrderBy(request.OrderBy);

			var nb = query.Count();
			var idProviders = await query.Skip(request.Skip.Value).Take(request.Take.Value).ToListAsync();
			return new OkObjectResult(new SearchResult<AuthenticationSchemeProviderResult>
			{
				Count = nb,
				Content = idProviders.Select(p => Build(p)).ToList()
			});
		}
		catch(OAuthException ex)
		{
			return BuildError(ex);
		}
	}

	[HttpDelete]
	public async Task<IActionResult> Remove([FromRoute] string prefix, string id)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
			var result = await _authenticationSchemeProviderRepository.Query()
				.Include(p => p.Realms)
				.Where(p => p.Realms.Any(r => r.Name == prefix))
				.SingleOrDefaultAsync(p => p.Name == id);
			if (result == null) return BuildError(System.Net.HttpStatusCode.NotFound, ErrorCodes.NOT_FOUND, string.Format(ErrorMessages.UNKNOWN_AUTH_SCHEME_PROVIDER, id));
			_authenticationSchemeProviderRepository.Remove(result);
			await _authenticationSchemeProviderRepository.SaveChanges(CancellationToken.None);
			return NoContent();
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] string prefix, string id)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
            var result = await _authenticationSchemeProviderRepository.Query()
                .Include(p => p.Realms)
                .Include(p => p.AuthSchemeProviderDefinition)
                .Include(p => p.Mappers)
                .Where(p => p.Realms.Any(r => r.Name == prefix))
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Name == id);
            if (result == null) return BuildError(System.Net.HttpStatusCode.NotFound, ErrorCodes.INVALID_REQUEST, string.Format(ErrorMessages.UNKNOWN_AUTH_SCHEME_PROVIDER, id));
			var optionKey = $"{result.Name}:{result.AuthSchemeProviderDefinition.OptionsName}";
            var optionType = Assembly.GetEntryAssembly().GetType(result.AuthSchemeProviderDefinition.OptionsFullQualifiedName);
            var section = _configuration.GetSection(optionKey);
            var configuration = section.Get(optionType);
			return new OkObjectResult(Build(result, configuration));
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

	[HttpPost]
	public async Task<IActionResult> Add([FromRoute] string prefix, [FromBody] AddAuthenticationSchemeProviderRequest request)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
			var instance = await _authenticationSchemeProviderRepository
                .Query()
				.Include(r => r.Realms)
				.AsNoTracking()
                .SingleOrDefaultAsync(a => a.Name == request.Name && a.Realms.Any(r => r.Name == prefix));
			if (instance != null) return BuildError(System.Net.HttpStatusCode.BadRequest, ErrorCodes.INVALID_REQUEST, ErrorMessages.AUTHSCHEMEPROVIDER_WITH_SAME_NAME_EXISTS);
			var idProviderDef = await _authenticationSchemeProviderDefinitionRepository.Query().SingleAsync(d => d.Name == request.DefinitionName);
			var realm = await _realmRepository.Query().SingleAsync(r => r.Name == prefix);
			var result = new AuthenticationSchemeProvider
			{
				Id = Guid.NewGuid().ToString(),
				AuthSchemeProviderDefinition = idProviderDef,
				CreateDateTime = DateTime.UtcNow,
				Description = request.Description,
				DisplayName = request.DisplayName,
				Mappers = Constants.GetDefaultIdProviderMappers(),
				Name = request.Name,
				UpdateDateTime = DateTime.UtcNow
			};
			result.Realms.Add(realm);
            SyncConfiguration(result, request.Values);
            _authenticationSchemeProviderRepository.Add(result);
			await _authenticationSchemeProviderRepository.SaveChanges(CancellationToken.None);
			return NoContent();
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

	[HttpPut]
	public async Task<IActionResult> UpdateDetails([FromRoute] string prefix, string id, [FromBody] UpdateAuthenticationSchemeProviderDetailsRequest request)
	{
		prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
            var instance = await _authenticationSchemeProviderRepository
                .Query()
                .Include(r => r.Realms)
                .SingleAsync(a => a.Name == id && a.Realms.Any(r => r.Name == prefix));
            if (instance == null) return BuildError(System.Net.HttpStatusCode.NotFound, ErrorCodes.NOT_FOUND, string.Format(ErrorMessages.UNKNOWN_AUTH_SCHEME_PROVIDER, id));
			instance.UpdateDateTime = DateTime.UtcNow;
			instance.Description = request.Description;
			instance.DisplayName = request.DisplayName;
            await _authenticationSchemeProviderRepository.SaveChanges(CancellationToken.None);
            return NoContent();
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateValues([FromRoute] string prefix, string id, [FromBody] UpdateAuthenticationSchemeProviderValuesRequest request)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
            var instance = await _authenticationSchemeProviderRepository
                .Query()
                .Include(r => r.AuthSchemeProviderDefinition)
                .Include(r => r.Realms)
                .SingleAsync(a => a.Name == id && a.Realms.Any(r => r.Name == prefix));
            if (instance == null) return BuildError(System.Net.HttpStatusCode.NotFound, ErrorCodes.NOT_FOUND, string.Format(ErrorMessages.UNKNOWN_AUTH_SCHEME_PROVIDER, id));
			instance.UpdateDateTime = DateTime.UtcNow;
            SyncConfiguration(instance, request.Values);
            await _authenticationSchemeProviderRepository.SaveChanges(CancellationToken.None);
            return NoContent();
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

	[HttpPost]
	public async Task<IActionResult> AddMapper([FromRoute] string prefix, string id, [FromBody] AddAuthenticationSchemeProviderMapperRequest request)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
            var instance = await _authenticationSchemeProviderRepository
                .Query()
                .Include(r => r.Realms)
                .Include(r => r.Mappers)
                .SingleOrDefaultAsync(a => a.Name == id && a.Realms.Any(r => r.Name == prefix));
            if (instance == null) return BuildError(System.Net.HttpStatusCode.NotFound, ErrorCodes.NOT_FOUND, string.Format(ErrorMessages.UNKNOWN_AUTH_SCHEME_PROVIDER, id));
            instance.UpdateDateTime = DateTime.UtcNow;
            var record = new AuthenticationSchemeProviderMapper
            {
                Id = id,
                MapperType = request.MapperType,
                Name = request.Name,
                SourceClaimName = request.SourceClaimName,
                TargetUserAttribute = request.TargetUserAttribute,
                TargetUserProperty = request.TargetUserProperty
            };
            instance.Mappers.Add(record);
            await _authenticationSchemeProviderRepository.SaveChanges(CancellationToken.None);
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Created,
                Content = JsonSerializer.Serialize(Build(record)),
                ContentType = "application/json"
            };
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveMapper([FromRoute] string prefix, string id, string mapperId)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
            var result = await _authenticationSchemeProviderRepository.Query()
                .Include(p => p.Realms)
                .Include(p => p.Mappers)
                .Where(p => p.Realms.Any(r => r.Name == prefix))
                .SingleOrDefaultAsync(p => p.Name == id);
            if (result == null) return BuildError(System.Net.HttpStatusCode.NotFound, ErrorCodes.INVALID_REQUEST, string.Format(ErrorMessages.UNKNOWN_AUTH_SCHEME_PROVIDER, id));
            result.Mappers = result.Mappers.Where(m => m.Id != mapperId).ToList();
            await _authenticationSchemeProviderRepository.SaveChanges(CancellationToken.None);
            return NoContent();
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMapper([FromRoute] string prefix, string id, string mapperId, [FromBody] UpdateAuthenticationSchemeProviderMapperRequest request)
    {
        prefix = prefix ?? Constants.DefaultRealm;
        try
        {
            CheckAccessToken(prefix, Constants.StandardScopes.AuthenticationSchemeProviders.Name, _jwtBuilder);
            var instance = await _authenticationSchemeProviderRepository
                .Query()
                .Include(r => r.Realms)
                .Include(r => r.Mappers)
                .SingleOrDefaultAsync(a => a.Name == id && a.Realms.Any(r => r.Name == prefix));
            if (instance == null) return BuildError(System.Net.HttpStatusCode.NotFound, ErrorCodes.NOT_FOUND, string.Format(ErrorMessages.UNKNOWN_AUTH_SCHEME_PROVIDER, id));
            instance.UpdateDateTime = DateTime.UtcNow;
            var mapper = instance.Mappers.Single(m => m.Id == mapperId);
            mapper.Name = request.Name;
            mapper.SourceClaimName = request.SourceClaimName;
            mapper.TargetUserAttribute = request.TargetUserAttribute;
            mapper.TargetUserProperty = request.TargetUserProperty;
            await _authenticationSchemeProviderRepository.SaveChanges(CancellationToken.None);
            return NoContent();
        }
        catch (OAuthException ex)
        {
            return BuildError(ex);
        }
    }

    private void SyncConfiguration(AuthenticationSchemeProvider authenticationSchemeProvider, Dictionary<string, string> values)
	{
		var optionKey = $"{authenticationSchemeProvider.Name}:{authenticationSchemeProvider.AuthSchemeProviderDefinition.OptionsName}";
		foreach(var kvp in values)
			_configuration[$"{optionKey}:{kvp.Key}"] = kvp.Value;
	}

	private static AuthenticationSchemeProviderResult Build(AuthenticationSchemeProvider authenticationSchemeProvider, object obj = null)
    {
        var values = new Dictionary<string, string>();
		if(obj != null)
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
                values.Add(property.Name, property.GetValue(obj).ToString());
        }

		return new AuthenticationSchemeProviderResult
		{
			CreateDateTime = authenticationSchemeProvider.CreateDateTime,
			Description = authenticationSchemeProvider.Description,
			DisplayName = authenticationSchemeProvider.DisplayName,
			Id = authenticationSchemeProvider.Id,
			Name = authenticationSchemeProvider.Name,
			UpdateDateTime = authenticationSchemeProvider.UpdateDateTime,
			Mappers = authenticationSchemeProvider.Mappers == null ? new List<AuthenticationSchemeProviderMapperResult>() : authenticationSchemeProvider.Mappers.Select(m => Build(m)).ToList(),
			Definition = authenticationSchemeProvider.AuthSchemeProviderDefinition == null ? null : new AuthenticationSchemeProviderDefinitionResult
            {
                Name = authenticationSchemeProvider.AuthSchemeProviderDefinition.Name,
                Description = authenticationSchemeProvider.AuthSchemeProviderDefinition.Description,
                Image = authenticationSchemeProvider.AuthSchemeProviderDefinition.Image,
                HandlerFullQualifiedName = authenticationSchemeProvider.AuthSchemeProviderDefinition.HandlerFullQualifiedName,
                OptionsFullQualifiedName = authenticationSchemeProvider.AuthSchemeProviderDefinition.OptionsFullQualifiedName,
                OptionsName = authenticationSchemeProvider.AuthSchemeProviderDefinition.OptionsName
            },
			Values = values
		};
	}

    private static AuthenticationSchemeProviderMapperResult Build(AuthenticationSchemeProviderMapper mapper)
    {
        return new AuthenticationSchemeProviderMapperResult
        {
            Id = mapper.Id,
            MapperType = mapper.MapperType,
            Name = mapper.Name,
            SourceClaimName = mapper.SourceClaimName,
            TargetUserAttribute = mapper.TargetUserAttribute,
            TargetUserProperty = mapper.TargetUserProperty
        };
    }
}