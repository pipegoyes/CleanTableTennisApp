using CleanTableTennisApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTableTennisApp.Application.IntegrationTests;

public class IntegrationTestServiceScope
{
    private readonly IServiceScope _scope = IntegrationTestLifeTime.ScopeFactory!.CreateScope();

    public TScopedService? GetService<TScopedService>()
    {
        var service = _scope.ServiceProvider.GetService<TScopedService>();
        return service;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        var mediator = _scope.ServiceProvider.GetService<ISender>();
        return await mediator?.Send(request)!;
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        var context = _scope.ServiceProvider.GetService<ApplicationDbContext>()!;
        return await context.FindAsync<TEntity>(keyValues);
    }

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        var context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        var dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var result = await action(_scope.ServiceProvider);
        return result;
    }

    public Task<T> ExecuteDbContextAsync<T>(Func<ApplicationDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<ApplicationDbContext>()!));

    public Task<int> InsertAsync<T>(params T[] entities) where T : class
    {
        return ExecuteDbContextAsync(db =>
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Add(entity);
            }
            return db.SaveChangesAsync();
        });
    }
}
