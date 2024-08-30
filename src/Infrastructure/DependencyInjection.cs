using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Infrastructure.Persistence;
using CleanTableTennisApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTableTennisApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanTableTennisAppDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlite(configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });

            });
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
