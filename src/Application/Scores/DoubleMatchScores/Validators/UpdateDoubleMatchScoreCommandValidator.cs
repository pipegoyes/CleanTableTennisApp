using CleanTableTennisApp.Application.Scores.DoubleMatchScores.Commands;
using CleanTableTennisApp.Application.Scores.Validators;
using FluentValidation;

namespace CleanTableTennisApp.Application.Scores.DoubleMatchScores.Validators;

public class UpdateDoubleMatchScoreCommandValidator : AbstractValidator<UpdateDoubleMatchScoresCommand>
{
    public UpdateDoubleMatchScoreCommandValidator()
    {
        RuleFor(s => s.DoubleMatchIdEncoded).NotNull();
        RuleFor(s => s.ScoreDtos).NotNull();
        RuleForEach(s => s.ScoreDtos).SetValidator(new ScoreDtoValidator());
    }
}
