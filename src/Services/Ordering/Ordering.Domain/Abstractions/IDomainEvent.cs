using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    
    public DateTime FiredAt => DateTime.UtcNow;

    public string EventType => GetType().AssemblyQualifiedName!;
}