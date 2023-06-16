using CleanTableTennisApp.Domain.Entities;
using FluentValidation;

namespace CleanTableTennisApp.Application.Scores.Validators;

public class ScoreValidator : AbstractValidator<ICollection<Score>>
{
    private const int NumberOfSetsToWinMatch = 3;
    private const int MaxNumberOfSetsPerMatch = 5;

    public ScoreValidator()
    {
        RuleFor(s => s)
            .Must(ValidateScores)
            .WithMessage($"Number of won ({NumberOfSetsToWinMatch}) sets exceed.");

        RuleFor(s => s)
            .Must(s => s.Count <= MaxNumberOfSetsPerMatch);
    }

    private bool ValidateScores(ICollection<Score> scores)
    {
        var hostWins = scores.Count(s => s.GamePointsHost > s.GamePointsGuest);
        var guestWins = scores.Count(s => s.GamePointsGuest > s.GamePointsHost);

        return hostWins <= NumberOfSetsToWinMatch && guestWins <= NumberOfSetsToWinMatch;
    }

}

// Todo there might be a way to unify those validators
public class DoubleScoreValidator : AbstractValidator<ICollection<DoubleMatchScore>>
{
    private const int NumberOfSetsToWinMatch = 3;
    private const int MaxNumberOfSetsPerMatch = 5;

    public DoubleScoreValidator()
    {
        RuleFor(s => s)
            .Must(ValidateScores)
            .WithMessage($"Number of won ({NumberOfSetsToWinMatch}) sets exceed.");

        RuleFor(s => s)
            .Must(s => s.Count <= MaxNumberOfSetsPerMatch);
    }

    private bool ValidateScores(ICollection<DoubleMatchScore> scores)
    {
        var hostWins = scores.Count(s => s.GamePointsHost > s.GamePointsGuest);
        var guestWins = scores.Count(s => s.GamePointsGuest > s.GamePointsHost);

        return hostWins <= NumberOfSetsToWinMatch && guestWins <= NumberOfSetsToWinMatch;
    }

}
