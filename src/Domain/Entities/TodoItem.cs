namespace CleanTableTennisApp.Domain.Entities;

// todo int for ids ?

public class Match : AuditableEntity
{
    public Match()
    {
    }

    public Match(TeamMatch teamMatch)
    {
        TeamMatch = teamMatch;
    }

    public int Id { get; set; }
    public TeamMatch? TeamMatch { get; set; }
}

public class TeamMatch : AuditableEntity
{
    public TeamMatch()
    {
    }

    public TeamMatch(Team hostTeam, Team guestTeam)
    {
        HostTeam = hostTeam;
        GuestTeam = guestTeam;
    }

    public int Id { get; set; }
    public Team? HostTeam { get; set; }
    public Team? GuestTeam { get; set; }
    public DateTime? FinishedAt { get; set; }
}

public class Team : AuditableEntity
{
    public Team(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}

public class Player: AuditableEntity
{
    public Player()
    {
    }

    public Player(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
}

public class TodoItem : AuditableEntity, IHasDomainEvent
{
    private bool _done;
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

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
