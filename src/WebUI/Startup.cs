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
using Microsoft.AspNetCore.Mvc;
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

        services.AddCors(builder =>
        {
            //todo use a dedicated policy and disable it in development Example .useCors(myProdPolicy)
            builder.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(new string[]
                {
                    "https://lively-tree-05d863103.3.azurestaticapps.net",
                    "http://localhost:4200"
                })
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
        
        services.AddExampleProviders(typeof(CreateTeamMatchExample).Assembly);

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => 
            options.SuppressModelStateInvalidFilter = true);

        
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
        //app.UseIdentityServer();
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
