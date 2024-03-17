using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Features.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("{DomainEvent} will be handled", domainEvent.GetType().Name);

        if (await featureManager.IsEnabledAsync("OrderFulfillment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}