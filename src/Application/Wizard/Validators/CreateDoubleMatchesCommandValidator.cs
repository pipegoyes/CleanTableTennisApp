using CleanTableTennisApp.Application.Wizard.Commands;
using CleanTableTennisApp.Domain.Enums;
using FluentValidation;

namespace CleanTableTennisApp.Application.Wizard.Validators;

public class CreateDoubleMatchesCommandValidator : AbstractValidator<CreateDoubleMatchesCommand>
{
    public CreateDoubleMatchesCommandValidator()
    {
        RuleFor(s => s.TeamMatchId).NotEqual(0);
        RuleFor(s => s.GuestPlayers)
            .NotNull()
            .Must(s => s.Count == 4)
            .Must(s => s.Count(s => s.DoublePosition == DoublePosition.FirstDouble) == 2)
            .Must(s => s.Count(s => s.DoublePosition == DoublePosition.SecondDouble) == 2);

        RuleFor(s => s.HostPlayers)
            .NotNull()
            .Must(s => s.Count == 4)
            .Must(s => s.Count(s => s.DoublePosition == DoublePosition.FirstDouble) == 2)
            .Must(s => s.Count(s => s.DoublePosition == DoublePosition.SecondDouble) == 2);
    }
}