using System.Security.Claims;
using CleanTableTennisApp.Application;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Infrastructure;
using CleanTableTennisApp.Infrastructure.Persistence;
using CleanTableTennisApp.WebUI.Endpoints;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using CleanTableTennisApp.WebUI.RequestExamples;
using CleanTableTennisApp.WebUI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using NSwag.Examples;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddApplication();
services.AddInfrastructure(config);
services.AddEndpoints<Program>(config);

services.AddSingleton<IAuthorizationHandler, HasPermissionsHandler>();
services.AddSingleton<ICurrentUserService, CurrentUserService>();

services.AddCors(corsOptions =>
{
    //todo use a dedicated policy and disable it in development Example .useCors(myProdPolicy)
    corsOptions.AddDefaultPolicy(policy =>
    {
        var originUrl = config.GetValue<string>("OriginUrl");
        policy.WithOrigins(originUrl!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
services.AddHttpContextAccessor();
services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
builder.Services.AddEndpointsApiExplorer();

//TODO Missing functionality ApiExceptionFilterAttribute wrap exception and throw expected error code
//services.AddControllersWithViews(options =>
//        options.Filters.Add<ApiExceptionFilterAttribute>())
//    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

services.AddExampleProviders(typeof(CreateTeamMatchExample).Assembly);

var domain = $"https://{config.GetValue<string>("Auth0:Domain")}/";
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = config.GetValue<string>("Auth0:Audience");
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

// todo missing ? AddOpenApiDocument
services.AddOpenApiDocument((configure, provider) =>
{
    configure.Title = "CleanTableTennisApp API";
    configure.Version = "1.0";
    //configure.AddExamples(provider);
});

services.AddSignalR();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
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

// todo app.UseStaticFiles();
app.UseOpenApi();
app.UseStaticFiles();
app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints<Program>();

// todo app.UseRouting();
// todo maphub is missing for minimal apis
// app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        "default",
//        "{controller}/{action=Index}/{id?}");
//    endpoints.MapRazorPages();
//    endpoints.MapHub<ScoresHub>("/real-time-scores");
//});

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

app.Run();
