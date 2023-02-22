namespace CleanTableTennisApp.Domain.Entities;

// todo int for ids ?

public class Match : AuditableEntity
{
    public int Id { get; set; }
    public TeamMatch TeamMatch { get; set; }

    public Match(TeamMatch teamMatch)
    {
        TeamMatch = teamMatch;
    }
}

public class TeamMatch : AuditableEntity
{
    public int Id { get; set; }
    public Team HostTeam { get; set; }
    public Team GuestTeam { get; set; }
    public DateTime? FinishedAt { get; set; }

    public TeamMatch(Team hostTeam, Team guestTeam)
    {
        HostTeam = hostTeam;
        GuestTeam = guestTeam;
    }
}

public class Team : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Team(string name)
    {
        Name = name;
    }

}

public class Player: AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Player(string name)
    {
        Name = name;
    }
}

public class TodoItem : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && _done == false)
            {
                DomainEvents.Add(new TodoItemCompletedEvent(this));
            }

            _done = value;
        }
    }

    public TodoList List { get; set; } = null!;

    public List<DomainEvent> DomainEvents { get; set; } = new();
}
