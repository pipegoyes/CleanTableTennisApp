using CleanTableTennisApp.Domain.Common;

namespace CleanTableTennisApp.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
