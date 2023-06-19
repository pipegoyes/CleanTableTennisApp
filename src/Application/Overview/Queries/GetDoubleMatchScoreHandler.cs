using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Overview.Queries;


public class GetDoubleMatchScoreQuery : IRequest<ScoreDto[]>
{
    public GetDoubleMatchScoreQuery(string doubleMatchIdEncoded)
    {
        DoubleMatchIdEncoded = doubleMatchIdEncoded;
    }

    public string DoubleMatchIdEncoded { get; set; }
}

public class GetDoubleMatchScoreHandler : IRequestHandler<GetDoubleMatchScoreQuery, ScoreDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;

    public GetDoubleMatchScoreHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder)
    {
        _context = context;
        _encoder = encoder;
    }

    public async Task<ScoreDto[]> Handle(GetDoubleMatchScoreQuery request, CancellationToken cancellationToken)
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