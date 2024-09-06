using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Common.Models;
using CleanTableTennisApp.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanTableTennisApp.Infrastructure.Services;

public class DomainEventService : IDomainEventService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IPublisher _mediator;

    public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Publish(DomainEvent domainEvent)
    {
#pragma warning disable CA1848
        _logger.LogInformation($"Publishing domain event. Event {domainEvent.GetType().Name}");
#pragma warning restore CA1848
        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private static INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}
