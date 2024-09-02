// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Infrastructure;
using CleanTableTennisApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CleanTableTennisApp.Application.IntegrationTests;

public class IntegrationTestLifeTime : IAsyncLifetime
{
    public static IServiceScopeFactory? ScopeFactory;
    private ApplicationDbContext? _dbContext;

    public async Task InitializeAsync()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = "Test"
        });

        var services = builder.Services;
        services.AddApplication();
        services.AddInfrastructure(builder.Configuration);

        services.ReplaceServiceWithSingletonMock<IHttpContextAccessor>();
        services.ReplaceServiceWithSingletonMock<ICurrentUserService>();

        var provider = services.BuildServiceProvider();
        ScopeFactory = provider.GetService<IServiceScopeFactory>() ?? throw new InvalidOperationException();

        var dbContext = provider.GetService<ApplicationDbContext>() ?? throw new InvalidOperationException("No Db found.");
        _dbContext = dbContext;

        await dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        if (_dbContext != null)
        {
            await _dbContext.Database.EnsureDeletedAsync();
        }
    }
}
