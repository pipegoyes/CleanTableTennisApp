using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Enums;
using FluentValidation;

namespace CleanTableTennisApp.Application.DoubleMatches.Validators;

public class DoublePlayerRequestValidator : AbstractValidator<DoublePlayerRequest>
{
    public DoublePlayerRequestValidator()
    {
        RuleFor(s => s.DoublePosition).NotEqual(DoublePosition.None);
    }
}
