using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Overview.Commands;

public class UpdateDoubleMatchScoreCommand : IRequest<bool>
{
    public string DoubleMatchIdEncoded { get; set; } = String.Empty;
    public IEnumerable<ScoreDto> ScoreDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}

public class UpdateDoubleMatchScoreHandler : IRequestHandler<UpdateDoubleMatchScoreCommand, bool>
{
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<ICollection<DoubleMatchScore>> _scoreValidator;

    public UpdateDoubleMatchScoreHandler(IUrlSafeIntEncoder encoder, IApplicationDbContext context, IValidator<ICollection<DoubleMatchScore>> scoreValidator)
    {
        _encoder = encoder;
        _context = context;
        _scoreValidator = scoreValidator;
    }

    public async Task<bool> Handle(UpdateDoubleMatchScoreCommand request, CancellationToken cancellationToken)
    {
        // todo find a way to re-use this code with UpdateMatchScoreHandler
        var doubleMatchIdDecoded = _encoder.Decode(request.DoubleMatchIdEncoded);
        var (newScores, updatedScores) = request.ScoreDtos.Split(s => s.ScoreIdEncoded.IsNullOrWhiteSpace());

        var match = await _context.DoubleMatches
            .Include(s => s.Scores)
            .FirstAsync(s => s.Id == doubleMatchIdDecoded, cancellationToken);

        foreach (var updatedScore in updatedScores)
        {
            var decodedId = _encoder.Decode(updatedScore.ScoreIdEncoded);
            var score = match.Scores.First(s => s.Id == decodedId);

            score.GamePointsGuest = updatedScore.GuestPoints;
            score.GamePointsHost = updatedScore.HostPoints;
        }

        foreach (var newScore in newScores)
        {
            match.Scores.Add(new DoubleMatchScore
            {
                GamePointsGuest = newScore.GuestPoints,
                GamePointsHost = newScore.HostPoints
            });
        }

        await _scoreValidator.ValidateAndThrowAsync(match.Scores, cancellationToken);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}