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
        var teamMatchWithSingleMatches = await _context.TeamMatches
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.HostPlayer)
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.GuestPlayer)
            .FirstAsync(s => s.Id == teamMatchId, cancellationToken);

        var teamMatchWithDoubleMatches = await _context.TeamMatches
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.HostPlayerLeft)
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.HostPlayerRight)
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.GuestPlayerLeft)
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.GuestPlayerRight)
            .FirstAsync(s => s.Id == teamMatchId, cancellationToken);

        return new OverviewDto
        {
            OverviewSingleMatchDtos = teamMatchWithSingleMatches.SingleMatches.Select(ToSingleMatchDto).ToList(),
            OverviewDoubleMatchDtos = teamMatchWithDoubleMatches.DoubleMatches.Select(toDoubleMatchDto).ToList(),
        };
    }

    private OverviewDoubleMatchDto toDoubleMatchDto(DoubleMatch doubleMatch)
    {
        return new OverviewDoubleMatchDto()
        {
            MatchIdEncoded = _encoder.Encode(doubleMatch.Id),
            HostLeftPlayerName = doubleMatch.HostPlayerLeft.Name,
            HostRightPlayerName = doubleMatch.HostPlayerRight.Name,
            GuestLeftPlayerName = doubleMatch.GuestPlayerLeft.Name,
            GuestRightPlayerName = doubleMatch.GuestPlayerRight.Name,
            HostPoints = doubleMatch.Scores.Count(s => s.GamePointsHost > s.GamePointsGuest),
            GuestPoints = doubleMatch.Scores.Count(s => s.GamePointsGuest > s.GamePointsHost)
        };
    }

    private OverviewSingleMatchDto ToSingleMatchDto(Match singleMatch)
    {
        return new OverviewSingleMatchDto
        {
            MatchIdEncoded = _encoder.Encode(singleMatch.Id),
            HostPlayerName = singleMatch.HostPlayer.Name,
            GuestPlayerName = singleMatch.GuestPlayer.Name,
            HostPoints = singleMatch.Scores.Count(s => s.GamePointsHost > s.GamePointsGuest),
            GuestPoints = singleMatch.Scores.Count(s => s.GamePointsGuest > s.GamePointsHost)
        };
    }
}