using FluentValidation;

namespace CleanTableTennisApp.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTeamMatchCommandValidator : AbstractValidator<CreateTeamMatchCommand>
{
    public CreateTeamMatchCommandValidator()
    {
        RuleFor(x => x.HostTeam).NotNull().SetValidator(new TeamRequestValidator());
        RuleFor(x => x.GuestTeam).NotNull().SetValidator(new TeamRequestValidator());
        RuleFor(s => s).Must(s => s.HostTeam.Name != s.GuestTeam.Name)
            .When(s => s.HostTeam != null && s.GuestTeam != null);

        // todo validate same name in two teams
    }
}

public class TeamRequestValidator : AbstractValidator<TeamRequest>
{
    public TeamRequestValidator()
    {
        RuleFor(s => s.Name).NotNull();
        RuleFor(s => s.Players)
            .NotNull()
            .Must(s => s.Count == 4)
            .Must(s => !IsPlayerNameRepeated(s))
            .Must(s => s.Where(s => s.DoublePosition == DoublePosition.FirstDouble).Count() == 2)
            .Must(s => s.Where(s => s.DoublePosition == DoublePosition.SecondDouble).Count() == 2);
        RuleForEach(s => s.Players).SetValidator(new PlayerRequestValidator());
    }

    public bool IsPlayerNameRepeated(IList<PlayerRequest> players)
    {
        var result = players.GroupBy(s => s.FullName).Any(s => s.Count() > 1);
        return result;
    }
}

public class PlayerRequestValidator : AbstractValidator<PlayerRequest>
{
    public PlayerRequestValidator()
    {
        RuleFor(x => x.FullName).NotNull();
        RuleFor(s => s.DoublePosition).IsInEnum();
    }
}

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
