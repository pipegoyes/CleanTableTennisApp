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

        // In production, the Angular files will be served from this directory
        services.AddSpaStaticFiles(configuration => 
            configuration.RootPath = "ClientApp/dist");

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
        if (!env.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }

        app.UseSwaggerUi3(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });

        app.UseSpa(spa =>
        {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer(Configuration["SpaBaseUrl"] ?? "http://localhost:4200");
            }
        });
    }
}
