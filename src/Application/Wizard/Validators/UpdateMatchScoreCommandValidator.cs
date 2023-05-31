using CleanTableTennisApp.Application.Wizard.Commands;
using FluentValidation;

namespace CleanTableTennisApp.Application.Wizard.Validators;

public class UpdateMatchScoreCommandValidator : AbstractValidator<UpdateMatchScoreCommand>
{
    public UpdateMatchScoreCommandValidator()
    {
        RuleFor(s => s.MatchIdEncoded).NotNull();
        RuleFor(s => s.ScoreRequests).NotNull();
        RuleForEach(s => s.ScoreRequests).SetValidator(new ScoreRequestValidator());
    }
}