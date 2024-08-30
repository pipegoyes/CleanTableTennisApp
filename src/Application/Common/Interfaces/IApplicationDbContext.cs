using CleanTableTennisApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Team> Teams { get; }
    DbSet<Player> Players { get; }
    DbSet<SingleMatch> SingleMatches { get; }
    DbSet<TeamMatch> TeamMatches { get; }
    DbSet<DoubleMatch> DoubleMatches { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
