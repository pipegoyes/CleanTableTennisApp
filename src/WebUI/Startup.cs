using System.Security.Claims;
using CleanTableTennisApp.Application;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Infrastructure;
using CleanTableTennisApp.Infrastructure.Persistence;
using CleanTableTennisApp.WebUI.Controllers;
using CleanTableTennisApp.WebUI.Filters;
using CleanTableTennisApp.WebUI.Permission;
using CleanTableTennisApp.WebUI.RequestExamples;
using CleanTableTennisApp.WebUI.Services;
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
        
        services.AddSingleton<IAuthorizationHandler, HasPermissionsHandler>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

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
                PermissionDto(Permissions.Write.Matches, domain)));
            options.AddPolicy(Permissions.All.Admin, policy => policy.Requirements.Add(new
                PermissionDto(Permissions.All.Admin, domain)));

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
