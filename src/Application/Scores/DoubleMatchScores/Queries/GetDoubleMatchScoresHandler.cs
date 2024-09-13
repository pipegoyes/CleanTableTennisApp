using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Scores.DoubleMatchScores.Queries;


public class GetDoubleMatchScoresQuery : IRequest<ScoreDto[]>
{
    public GetDoubleMatchScoresQuery(string doubleMatchIdEncoded)
    {
        DoubleMatchIdEncoded = doubleMatchIdEncoded;
    }

    public string DoubleMatchIdEncoded { get; set; }
}

public class GetDoubleMatchScoresHandler : IRequestHandler<GetDoubleMatchScoresQuery, ScoreDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;

    public GetDoubleMatchScoresHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder)
    {
        _context = context;
        _encoder = encoder;
    }

    public async Task<ScoreDto[]> Handle(GetDoubleMatchScoresQuery request, CancellationToken cancellationToken)
    {
        var doubleMatchId = _encoder.Decode(request.DoubleMatchIdEncoded);

        var doubleMatch = await _context.DoubleMatches
            .Include(s => s.Scores)
            .FirstAsync(s => s.Id == doubleMatchId, cancellationToken);

        return doubleMatch.Scores.Select(ToScoreDto).ToArray();
    }

    private ScoreDto ToScoreDto(DoubleMatchScore score)
    {
        return new ScoreDto
        {
            GuestPoints = score.GamePointsGuest,
            HostPoints = score.GamePointsHost,
            ScoreIdEncoded = _encoder.Encode(score.Id)
        };
    }
}
