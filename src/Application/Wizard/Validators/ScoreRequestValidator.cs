using CleanTableTennisApp.Application.Requests;
using FluentValidation;

namespace CleanTableTennisApp.Application.Wizard.Validators;

public class ScoreRequestValidator : AbstractValidator<ScoreRequest>
{
    private const int TenPoints = 10;
    private const int ElevenPoints = 11;

    public ScoreRequestValidator()
    {
        RuleFor(score => score)
            .Must(score => ValidatePointsDifference(score.GuestPoints, score.HostPoints))
            .WithMessage("The difference between HostPoints and GuestPoints must be at least 2.");

        RuleFor(score => score)
            .Must(score => ValidateMinimumScore(score.HostPoints, score.GuestPoints))
            .WithMessage("At least one of the scores must be 11 when both players have less than 10 points.");

    }

    private bool ValidatePointsDifference(int guestPoints, int hostPoints)
    {
        if (guestPoints >= TenPoints && hostPoints >= TenPoints)
        {
            return Math.Abs(guestPoints - hostPoints) >= 2;
        }

        return true;
    }

    private bool ValidateMinimumScore(int hostPoints, int guestPoints)
    {
        if (hostPoints < TenPoints || guestPoints < TenPoints)
        {
            return (hostPoints == ElevenPoints && guestPoints < TenPoints) || (hostPoints < TenPoints && guestPoints == ElevenPoints);
        }
        return true;
    }
}