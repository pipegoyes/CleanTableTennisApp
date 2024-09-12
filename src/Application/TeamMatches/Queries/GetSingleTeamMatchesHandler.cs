using CleanTableTennisApp.Application.Common.Converters;
using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Home.Queries;

public class GetSingleTeamMatchesQuery : IRequest<TeamMatchDto>
{
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
}

public class GetSingleTeamMatchesHandler : IRequestHandler<GetSingleTeamMatchesQuery, TeamMatchDto>
{
    
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly ITeamMatchConverter _teamMatchConverter;

    public GetSingleTeamMatchesHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder, ITeamMatchConverter teamMatchConverter)
    {
        _context = context;
        _encoder = encoder;
        _teamMatchConverter = teamMatchConverter;
    }

    public async Task<TeamMatchDto> Handle(GetSingleTeamMatchesQuery request, CancellationToken cancellationToken)
    {
        var teamMatchId = _encoder.Decode(request.TeamMatchIdEncoded);

        var teamMatch = await _context.TeamMatches
            .Include(s => s.GuestTeam)
            .Include(s => s.HostTeam)
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.Scores)
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.Scores)
            .FirstAsync(s => s.Id == teamMatchId, cancellationToken: cancellationToken);

        return _teamMatchConverter.ToDto(teamMatch);
    }
} 
