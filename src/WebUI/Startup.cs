using System.Security.Claims;
using CleanTableTennisApp.Application;
using CleanTableTennisApp.Application.Common.Converters;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Overview;
using CleanTableTennisApp.Application.Scores;
using CleanTableTennisApp.Application.Scores.Validators;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Infrastructure;
using CleanTableTennisApp.Infrastructure.Persistence;
using CleanTableTennisApp.WebUI.Controllers;
using CleanTableTennisApp.WebUI.Filters;
using CleanTableTennisApp.WebUI.RequestExamples;
using CleanTableTennisApp.WebUI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Examples;
using NSwag.Generation.Processors.Security;

namespace CleanTableTennisApp.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddDatabaseDeveloperPageExceptionFilter();

        // todo some of them needs to go to AddApplication or AddInfrastructure
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IUrlSafeIntEncoder, UrlSafeIntEncoder>();
        services.AddSingleton<IValidator<ICollection<Score>>, ScoreValidator>();
        services.AddSingleton<IValidator<ICollection<DoubleMatchScore>>, DoubleScoreValidator>();
        services.AddSingleton<IVictoriesCounter, VictoriesCounter>();
        services.AddSingleton<IGamePointsUpdater<Score>, GamePointsUpdater<Score>>();
        services.AddSingleton<IGamePointsUpdater<DoubleMatchScore>, GamePointsUpdater<DoubleMatchScore>>();
        services.AddSingleton<ITeamMatchConverter, TeamMatchConverter>();
        services.AddSingleton<IScoreDtoConverter, ScoreDtoConverter>();
        services.AddSingleton<ITeamMatchVictoriesCounter, TeamMatchVictoriesCounter>();
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        services.AddCors(builder =>
        {
            //todo use a dedicated policy and disable it in development Example .useCors(myProdPolicy)
            builder.AddDefaultPolicy(policy =>
            {
                var originUrl = Configuration.GetValue<string>("OriginUrl");
                policy.WithOrigins(originUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        services.AddExampleProviders(typeof(CreateTeamMatchExample).Assembly);

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        var domain = $"https://{Configuration.GetValue<string>("Auth0:Domain")}/";
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration.GetValue<string>("Auth0:Audience");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Permissions.Write.Matches, policy => policy.Requirements.Add(new
                HasScopeRequirement(Permissions.Write.Matches, domain)));
            options.AddPolicy(Permissions.All.Admin, policy => policy.Requirements.Add(new
                HasScopeRequirement(Permissions.All.Admin, domain)));
        });

        services.AddOpenApiDocument((configure, provider) =>
        {
            configure.Title = "CleanTableTennisApp API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            configure.AddExamples(provider);
        });
        services.AddSignalR();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSwaggerUi(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        app.UseRouting();
        
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
            endpoints.MapHub<ScoresHub>("/real-time-scores");
        });

    }
}

public static class Permissions
{
    public static class Write
    {
        public const string Matches = "write:matches";
    }

    public static class All
    {
        public const string Admin = "all:admin";
    }
}

public class HasScopeRequirement : IAuthorizationRequirement
{
    public string Issuer { get; }
    public string Scope { get; }

    public HasScopeRequirement(string scope, string issuer)
    {
        Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
    }
}

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
    {
        // If user does not have the scope claim, get out of here
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        // Split the scopes string into an array
        var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)?.Value.Split(' ');

        // Succeed if the scope array contains the required scope
        if (scopes != null && scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
