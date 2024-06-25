using CleanTableTennisApp.Infrastructure.Identity;
using CleanTableTennisApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.WebUI;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                //TODO this should be reactivated
                //if (context.Database.IsSqlServer())
                //{
                //    await context.Database.MigrateAsync();
                //}

                //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                //await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
                await ApplicationDbContextSeed.SeedSampleDataAsync(context);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var loggerMessage = LoggerMessage.Define(LogLevel.Error, 1, "An error occurred while migrating or seeding the database.");
                loggerMessage(logger, ex);
                throw;
            }
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>());
    }
}
