using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Home.Queries;

public class GetSingleTeamMatchesQuery : IRequest<TeamMatch>
{
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
}

public class GetSingleTeamMatchesHandler : IRequestHandler<GetSingleTeamMatchesQuery, TeamMatch>
{
    private readonly IApplicationDbContext _context;

    public GetSingleTeamMatchesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TeamMatch> Handle(GetSingleTeamMatchesQuery request, CancellationToken cancellationToken)
    {
        var teamMatchId = TeamMatch.Decode(request.TeamMatchIdEncoded);

        var teamMatch = await _context.TeamMatches
            .Include(s => s.GuestTeam)
            .Include(s => s.HostTeam)
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.Scores)
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.Scores)
            .FirstAsync(s => s.Id == teamMatchId, cancellationToken: cancellationToken);

        return teamMatch;
    }
} 
