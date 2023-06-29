using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Commands;

public class CreateEmptyDoubleScoresCommand : IRequest<bool>
{
    public int TeamMatchId { get; set; }
}

public class CreateEmptyDoubleScoresHandler : IRequestHandler<CreateEmptyDoubleScoresCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CreateEmptyDoubleScoresHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateEmptyDoubleScoresCommand request, CancellationToken cancellationToken)
    {
        var teamMatch = await _context.TeamMatches
            .Include(s => s.DoubleMatches)
            .FirstAsync(s => s.Id == request.TeamMatchId, cancellationToken);

        foreach (var doubleMatch in teamMatch.DoubleMatches)
        {
            var firstScore = CreateEmptyScore(doubleMatch);
            var secondScore = CreateEmptyScore(doubleMatch);
            var thirdScore = CreateEmptyScore(doubleMatch);
            var fourth = CreateEmptyScore(doubleMatch);
            var fifth = CreateEmptyScore(doubleMatch);

            doubleMatch.Scores = new List<DoubleMatchScore>
            {
                firstScore, secondScore, thirdScore, fourth, fifth
            };  
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    private static DoubleMatchScore CreateEmptyScore(DoubleMatch doubleMatch)
    {
        return new DoubleMatchScore { GamePointsGuest = 0, GamePointsHost = 0, DoubleMatch = doubleMatch};
    }
}