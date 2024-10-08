using CleanTableTennisApp.Application.Common.Converters;
using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Scores.SingleMatchScores;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.TeamMatches.Queries;

public class GetTeamMatchOverviewQuery : IRequest<TeamMatchOverviewDto>
{
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
}

public class GetTeamMatchOverviewHandler : IRequestHandler<GetTeamMatchOverviewQuery, TeamMatchOverviewDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IVictoriesCounter _victoriesCounter;
    private readonly IScoreDtoConverter _scoreDtoConverter;

    public GetTeamMatchOverviewHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder, IVictoriesCounter victoriesCounter, IScoreDtoConverter scoreDtoConverter)
    {
        _context = context;
        _encoder = encoder;
        _victoriesCounter = victoriesCounter;
        _scoreDtoConverter = scoreDtoConverter;
    }

    public async Task<TeamMatchOverviewDto> Handle(GetTeamMatchOverviewQuery request, CancellationToken cancellationToken)
    {
        var teamMatchId = _encoder.Decode(request.TeamMatchIdEncoded);
        var teamMatchWithSingleMatches = await _context.TeamMatches
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.HostPlayer)
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.GuestPlayer)
            .Include(s => s.SingleMatches)
            .ThenInclude(s => s.Scores)
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
            .Include(s => s.DoubleMatches)
            .ThenInclude(s => s.Scores)
            .FirstAsync(s => s.Id == teamMatchId, cancellationToken);

        return new TeamMatchOverviewDto
        {
            OverviewSingleMatchDtos = teamMatchWithSingleMatches.SingleMatches.Select(ToSingleMatchDto).OrderBy(s => s.PlayingOrder).ToList(),
            OverviewDoubleMatchDtos = teamMatchWithDoubleMatches.DoubleMatches.Select(ToDoubleMatchDto).OrderBy(s => s.PlayingOrder).ToList(),
        };
    }

    private OverviewDoubleMatchDto ToDoubleMatchDto(DoubleMatch doubleMatch)
    {
        return new OverviewDoubleMatchDto
        {
            MatchIdEncoded = _encoder.Encode(doubleMatch.Id),
            HostLeftPlayerName = doubleMatch.HostPlayerLeft.Name,
            HostRightPlayerName = doubleMatch.HostPlayerRight.Name,
            GuestLeftPlayerName = doubleMatch.GuestPlayerLeft.Name,
            GuestRightPlayerName = doubleMatch.GuestPlayerRight.Name,
            PlayingOrder = doubleMatch.PlayingOrder,
            HostPoints = _victoriesCounter.HostVictoriesCounter(doubleMatch.Scores.ToList()),
            GuestPoints = _victoriesCounter.GuestVictoriesCounter(doubleMatch.Scores.ToList()),
            ScoresDtos = doubleMatch.Scores.Select(s => _scoreDtoConverter.ToDto(s)),
        };
    }

    private OverviewSingleMatchDto ToSingleMatchDto(SingleMatch singleMatch)
    {
        return new OverviewSingleMatchDto
        {
            MatchIdEncoded = _encoder.Encode(singleMatch.Id),
            HostPlayerName = singleMatch.HostPlayer.Name,
            GuestPlayerName = singleMatch.GuestPlayer.Name,
            ScoresDtos = singleMatch.Scores.Select(_scoreDtoConverter.ToDto),
            PlayingOrder = singleMatch.PlayingOrder,
            HostPoints = _victoriesCounter.HostVictoriesCounter(singleMatch.Scores.ToList()),
            GuestPoints = _victoriesCounter.GuestVictoriesCounter(singleMatch.Scores.ToList()),
        };
    }
}
