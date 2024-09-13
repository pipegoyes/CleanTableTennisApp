using CleanTableTennisApp.Application.TeamMatches.Command;
using CleanTableTennisApp.Application.Wizard.Validators;
using FluentValidation;

namespace CleanTableTennisApp.Application.TeamMatches.Validators;

public class CreateTeamMatchCommandValidator : AbstractValidator<CreateTeamMatchCommand>
{
    public CreateTeamMatchCommandValidator()
    {
        RuleFor(x => x.HostTeam).NotNull().SetValidator(new TeamRequestValidator());
        RuleFor(x => x.GuestTeam).NotNull().SetValidator(new TeamRequestValidator());
        RuleFor(s => s).Must(s => s.HostTeam.Name != s.GuestTeam.Name);
    }
}
