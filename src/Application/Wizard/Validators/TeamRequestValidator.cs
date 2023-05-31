using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;
using FluentValidation;

namespace CleanTableTennisApp.Application.Wizard.Validators;

public class TeamRequestValidator : AbstractValidator<TeamRequest>
{
    public TeamRequestValidator()
    {
        RuleFor(s => s.Name).NotNull();
        RuleFor(s => s.Players)
            .NotNull()
            .Must(s => s.Count == 4)
            .Must(s => !IsPlayerNameRepeated(s))
            .Must(s => s.Count(s => s.DoublePosition == DoublePosition.FirstDouble) == 2)
            .Must(s => s.Count(s => s.DoublePosition == DoublePosition.SecondDouble) == 2);
        RuleForEach(s => s.Players).SetValidator(new PlayerRequestValidator());
    }

    public bool IsPlayerNameRepeated(IList<PlayerRequest> players)
    {
        var result = players.GroupBy(s => s.FullName).Any(s => s.Count() > 1);
        return result;
    }
}

public class ScoresValidator : AbstractValidator<ICollection<Score>>
{
    private const int NumberOfSetsToWinMatch = 3;
    private const int MaxNumberOfSetsPerMatch = 5;

    public ScoresValidator()
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