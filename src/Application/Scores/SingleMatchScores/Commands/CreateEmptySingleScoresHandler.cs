using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Scores.SingleMatchScores.Commands;

public class CreateEmptySingleScoresCommand : IRequest<bool>
{
    public int TeamMatchId { get; set; }
}

public class CreateEmptySingleScoresHandler : IRequestHandler<CreateEmptySingleScoresCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CreateEmptySingleScoresHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateEmptySingleScoresCommand request, CancellationToken cancellationToken)
    {
        var teamMatch = await _context.TeamMatches
            .Include(s => s.SingleMatches)
            .FirstAsync(s => s.Id == request.TeamMatchId, cancellationToken);

        foreach (var singleMatch in teamMatch.SingleMatches)
        {
            AddScoreToMatch(singleMatch);
            AddScoreToMatch(singleMatch);
            AddScoreToMatch(singleMatch);
            AddScoreToMatch(singleMatch);
            AddScoreToMatch(singleMatch);
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    private static void AddScoreToMatch(SingleMatch singleMatch)
    {
        var score = new SingleMatchScore { GamePointsGuest = 0, GamePointsHost = 0, };
        singleMatch.Scores.Add(score);
    }
}
