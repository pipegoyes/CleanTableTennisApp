using CleanTableTennisApp.Application.TeamMatch.Commands;
using FluentValidation;

namespace CleanTableTennisApp.Application.TeamMatch.Validators;

public class CreateTeamMatchCommandValidator : AbstractValidator<CreateTeamMatchCommand>
{
    public CreateTeamMatchCommandValidator()
    {
        RuleFor(x => x.HostTeam).NotNull().SetValidator(new TeamRequestValidator());
        RuleFor(x => x.GuestTeam).NotNull().SetValidator(new TeamRequestValidator());
        RuleFor(s => s).Must(s => s.HostTeam.Name != s.GuestTeam.Name);

        // todo validate same name in two teams
    }
}