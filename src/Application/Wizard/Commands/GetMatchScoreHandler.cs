using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Commands;

public class GetMatchScoreQuery : IRequest<ScoreDto[]>
{
    public GetMatchScoreQuery(string matchIdEncoded)
    {
        MatchIdEncoded = matchIdEncoded;
    }

    public string MatchIdEncoded { get; set; }
}

public class GetMatchScoreHandler : IRequestHandler<GetMatchScoreQuery, ScoreDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;

    public GetMatchScoreHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder)
    {
        _context = context;
        _encoder = encoder;
    }

    public async Task<ScoreDto[]> Handle(GetMatchScoreQuery request, CancellationToken cancellationToken)
    {
        var decodedMatchId = _encoder.Decode(request.MatchIdEncoded);
        var match =  await _context.SingleMatches
            .Include(s => s.Scores)
            .FirstAsync(s => s.Id == decodedMatchId, cancellationToken);

        var result = match.Scores.Select(ToScoreDto).ToArray();
        return result;
    }

    private ScoreDto ToScoreDto(Score score)
    {
        return new ScoreDto
        {
            HostPoints = score.GamePointsHost, 
            GuestPoints = score.GamePointsGuest, 
            ScoreIdEncoded = _encoder.Encode(score.Id)
        };
    }
}