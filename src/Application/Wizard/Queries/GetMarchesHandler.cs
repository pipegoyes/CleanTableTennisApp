using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Queries;

public class GetMatchesQuery : IRequest<OverviewDto>
{
    public string TeamMatchIdEncoded { get; set; }
}

public class GetMarchesHandler : IRequestHandler<GetMatchesQuery, OverviewDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder; 

    public GetMarchesHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder)
    {
        _context = context;
        _encoder = encoder;
    }

    public async Task<OverviewDto> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
    {
        var teamMatchId = _encoder.Decode(request.TeamMatchIdEncoded);
        var teamMatch = await _context.TeamMatches
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.HostPlayer)
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.GuestPlayer)
            .FirstAsync(s => s.Id == teamMatchId, cancellationToken);
        return new OverviewDto { OverviewSingleMatchDtos = teamMatch.SingleMatches.Select(ToOverviewSingleMatchDto).ToList() };
    }

    private OverviewSingleMatchDto ToOverviewSingleMatchDto(Match singleMatch)
    {
        return new OverviewSingleMatchDto
        {
            MatchId = singleMatch.Id,
            HostPlayerName = singleMatch.HostPlayer.Name,
            GuestPlayerName = singleMatch.GuestPlayer.Name,
            HostPoints = singleMatch.Scores.Count(s => s.GamePointsHost > s.GamePointsGuest),
            GuestPoints = singleMatch.Scores.Count(s => s.GamePointsGuest > s.GamePointsHost)
        };
    }
}