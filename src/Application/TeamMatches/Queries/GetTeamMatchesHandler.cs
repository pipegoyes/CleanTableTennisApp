using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Home.Queries;

// todo IreadOnly collection ?
public class GetTeamMatchesQuery : IRequest<TeamMatch[]>
{
}


public class GetTeamMatchesHandler : IRequestHandler<GetTeamMatchesQuery, TeamMatch[]>
{
    private readonly IApplicationDbContext _context;

    public GetTeamMatchesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TeamMatch[]> Handle(GetTeamMatchesQuery request, CancellationToken cancellationToken)
    {
        var teamMatches = await _context.TeamMatches
            .Include(s => s.GuestTeam)
            .Include(s => s.HostTeam)
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.Scores)
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.Scores)
            .Where(s => s.FinishedAt == null)
            .ToArrayAsync(cancellationToken: cancellationToken);

        return teamMatches.ToArray();

    }
}

