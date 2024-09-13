using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Infrastructure.Persistence;

public class DbInitializer(ApplicationDbContext context)
{
    public async Task InitializeAsync()
    {
        await context.Database.MigrateAsync();
    }
}
