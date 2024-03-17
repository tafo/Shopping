namespace Ordering.Application.Features.EventHandlers.Domain;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order updated event is handled: {DomainEvent}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}
