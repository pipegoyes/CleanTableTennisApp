using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Commands;

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
        var teamMatch = await _context.TeamMatches.FirstAsync(s => s.Id == request.TeamMatchId, cancellationToken);

        foreach (Match singleMatch in teamMatch.SingleMatches)
        {
            var score = new Score() { GamePointsGuest = 0, GamePointsHost = 0, };
            singleMatch.Scores.Add(score);
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}