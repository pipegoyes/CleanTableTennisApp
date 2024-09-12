using CleanTableTennisApp.Application.Common.Converters;
using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Home.Queries;

// todo IreadOnly collection ?
public class GetTeamMatchesQuery : IRequest<TeamMatchDto[]>
{
}


public class GetTeamMatchesHandler : IRequestHandler<GetTeamMatchesQuery, TeamMatchDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly ITeamMatchConverter _teamMatchConverter;

    public GetTeamMatchesHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder, ITeamMatchConverter teamMatchConverter)
    {
        _context = context;
        _encoder = encoder;
        _teamMatchConverter = teamMatchConverter;
    }

    public async Task<TeamMatchDto[]> Handle(GetTeamMatchesQuery request, CancellationToken cancellationToken)
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

        return teamMatches.Select(_teamMatchConverter.ToDto).ToArray();

    }
}

