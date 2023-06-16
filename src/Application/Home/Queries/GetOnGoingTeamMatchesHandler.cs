using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Home.Queries;

public class GetOnGoingTeamMatchesQuery : IRequest<TeamMatchDto[]>
{
}

public class GetOnGoingTeamMatchesHandler : IRequestHandler<GetOnGoingTeamMatchesQuery, TeamMatchDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;

    public GetOnGoingTeamMatchesHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder)
    {
        _context = context;
        _encoder = encoder;
    }

    public async Task<TeamMatchDto[]> Handle(GetOnGoingTeamMatchesQuery request, CancellationToken cancellationToken)
    {
        var teamMatches = await _context.TeamMatches
            .Include(s => s.GuestTeam)
            .Include(s => s.HostTeam)
            .Where(s => s.FinishedAt == null)
            .ToArrayAsync(cancellationToken: cancellationToken);

        return teamMatches.Select(ToDto).ToArray();

    }

    private TeamMatchDto ToDto(TeamMatch match)
    {
        return new TeamMatchDto
        {
            GuestTeamName = match.GuestTeam.Name,
            HostTeamName = match.HostTeam.Name,
            GuestVictories = 0,
            HostVictories = 0,
            StartedAt = match.Created,
            TeamMatchIdEncoded = _encoder.Encode(match.Id)
        };
    }
}