using CleanTableTennisApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // todo to be removed

    DbSet<Team> Teams { get; }
    DbSet<Player> Players { get; }
    DbSet<Match> Matches { get; }
    DbSet<TeamMatch> TeamMatches { get; }
    DbSet<DoubleMatch> DoubleMatches { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
