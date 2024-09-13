using CleanTableTennisApp.Application.Scores.SingleMatchScores.Commands;
using FluentValidation;

namespace CleanTableTennisApp.Application.Scores.Validators;

public class UpdateMatchScoreCommandValidator : AbstractValidator<UpdateSingleMatchScoreCommand>
{
    public UpdateMatchScoreCommandValidator()
    {
        RuleFor(s => s.MatchIdEncoded).NotNull();
        RuleFor(s => s.ScoreDtos).NotNull();
        RuleForEach(s => s.ScoreDtos).SetValidator(new ScoreDtoValidator());
    }
}
