using CleanTableTennisApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.WebUI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
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
