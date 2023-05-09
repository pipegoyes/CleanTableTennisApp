using CleanTableTennisApp.Application.Requests;
using FluentValidation;

namespace CleanTableTennisApp.Application.Wizard.Validators;

public class PlayerRequestValidator : AbstractValidator<PlayerRequest>
{
    public PlayerRequestValidator()
    {
        RuleFor(x => x.FullName).NotNull();
        RuleFor(s => s.DoublePosition).IsInEnum();
    }
}