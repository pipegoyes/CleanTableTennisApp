using CleanTableTennisApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Team> Teams { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
