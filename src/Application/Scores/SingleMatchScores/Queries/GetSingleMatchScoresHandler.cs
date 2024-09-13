using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Scores.SingleMatchScores.Queries;

public class GetSingleMatchScoresQuery : IRequest<ScoreDto[]>
{
    public GetSingleMatchScoresQuery(string matchIdEncoded)
    {
        MatchIdEncoded = matchIdEncoded;
    }

    public string MatchIdEncoded { get; set; }
}

public class GetSingleMatchScoresHandler : IRequestHandler<GetSingleMatchScoresQuery, ScoreDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;

    public GetSingleMatchScoresHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder)
    {
        _context = context;
        _encoder = encoder;
    }

    public async Task<ScoreDto[]> Handle(GetSingleMatchScoresQuery request, CancellationToken cancellationToken)
    {
        var decodedMatchId = _encoder.Decode(request.MatchIdEncoded);
        var match = await _context.SingleMatches
            .Include(s => s.Scores)
            .FirstAsync(s => s.Id == decodedMatchId, cancellationToken);

        var result = match.Scores.Select(ToScoreDto).ToArray();
        return result;
    }

    private ScoreDto ToScoreDto(SingleMatchScore score)
    {
        return new ScoreDto
        {
            HostPoints = score.GamePointsHost,
            GuestPoints = score.GamePointsGuest,
            ScoreIdEncoded = _encoder.Encode(score.Id)
        };
    }
}
