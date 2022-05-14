using CleanArchitectureSolution.Domain.Common;

namespace CleanArchitectureSolution.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
