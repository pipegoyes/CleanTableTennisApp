using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.Wizard.Validators;
using CleanTableTennisApp.Domain.Enums;
using FluentValidation;

namespace CleanTableTennisApp.Application.TeamMatches.Validators;

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
