using CleanTableTennisApp.Application.Common.Converters;
using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Home.Queries;

// todo IreadOnly collection ?
public class GetOnGoingTeamMatchesQuery : IRequest<TeamMatchDto[]>
{
}


public class GetOnGoingTeamMatchesHandler : IRequestHandler<GetOnGoingTeamMatchesQuery, TeamMatchDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly ITeamMatchConverter _teamMatchConverter;

    public GetOnGoingTeamMatchesHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder, ITeamMatchConverter teamMatchConverter)
    {
        _context = context;
        _encoder = encoder;
        _teamMatchConverter = teamMatchConverter;
    }

    public async Task<TeamMatchDto[]> Handle(GetOnGoingTeamMatchesQuery request, CancellationToken cancellationToken)
    {
        var teamMatches = await _context.TeamMatches
            .Include(s => s.GuestTeam)
            .Include(s => s.HostTeam)
            .Where(s => s.FinishedAt == null)
            .ToArrayAsync(cancellationToken: cancellationToken);

        return teamMatches.Select(s => _teamMatchConverter.ToDto(s)).ToArray();

    }
}