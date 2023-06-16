using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Wizard.Validators;
using FluentValidation;

namespace CleanTableTennisApp.Application.Scores.Validators;

public class UpdateDoubleMatchScoreCommandValidator : AbstractValidator<UpdateDoubleMatchScoreCommand>
{
    public UpdateDoubleMatchScoreCommandValidator()
    {
        RuleFor(s => s.DoubleMatchIdEncoded).NotNull();
        RuleFor(s => s.ScoreDtos).NotNull();
        RuleForEach(s => s.ScoreDtos).SetValidator(new ScoreDtoValidator());
    }
}