namespace CleanTableTennisApp.Domain.Entities;

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
