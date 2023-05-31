using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Commands;


public class UpdateMatchScoreCommand : IRequest<bool>
{
    public string MatchIdEncoded { get; set; } = string.Empty;
    public IEnumerable<ScoreRequest> ScoreRequests { get; set; }
}

public class UpdateMatchScoreHandler : IRequestHandler<UpdateMatchScoreCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IValidator<ICollection<Score>> _scoreValidator;

    public UpdateMatchScoreHandler(IApplicationDbContext context, IUrlSafeIntEncoder encoder, IValidator<ICollection<Score>> scoreValidator)
    {
        _context = context;
        _encoder = encoder;
        _scoreValidator = scoreValidator;
    }

    public async Task<bool> Handle(UpdateMatchScoreCommand request, CancellationToken cancellationToken)
    {
        var matchIdDecoded = _encoder.Decode(request.MatchIdEncoded);
        var (newScores, updatedScores) = request.ScoreRequests.Split(s => s.ScoreIdEncoded.IsNullOrWhiteSpace());

        var match = await _context.Matches
            .Include(s => s.Scores)
            .FirstAsync(s => s.Id == matchIdDecoded, cancellationToken);

        foreach (var updatedScore in updatedScores)
        {
            var decodedId = _encoder.Decode(updatedScore.ScoreIdEncoded);
            var score = match.Scores.First(s => s.Id == decodedId);

            score.GamePointsGuest = updatedScore.GuestPoints;
            score.GamePointsHost = updatedScore.HostPoints;
        }

        foreach (var newScore in newScores)
        {
            match.Scores.Add(new Score
            {
                GamePointsGuest = newScore.GuestPoints,
                GamePointsHost = newScore.HostPoints
            });
        }

        await _scoreValidator.ValidateAndThrowAsync(match.Scores, cancellationToken);

        return await _context.SaveChangesAsync(cancellationToken) > 0;

    }
}

public static class Require
{
    public static void IsNotNull<T>(T value)
    {
        if (value is null)
        {
            throw new ArgumentException($"{nameof(value)} must be not null.");
        }
    }
}