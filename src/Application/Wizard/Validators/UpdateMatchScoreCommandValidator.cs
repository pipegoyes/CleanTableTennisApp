using CleanTableTennisApp.Application.Overview.Commands;
using FluentValidation;

namespace CleanTableTennisApp.Application.Wizard.Validators;

public class UpdateMatchScoreCommandValidator : AbstractValidator<UpdateMatchScoreCommand>
{
    public UpdateMatchScoreCommandValidator()
    {
        RuleFor(s => s.MatchIdEncoded).NotNull();
        RuleFor(s => s.ScoreDtos).NotNull();
        RuleForEach(s => s.ScoreDtos).SetValidator(new ScoreDtoValidator());
    }
}