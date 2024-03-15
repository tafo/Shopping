namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainEventList = [];

    public IReadOnlyList<IDomainEvent> DomainEventList => _domainEventList.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEventList.Add(domainEvent);
    }
    
    public IDomainEvent[] ClearDomainEvents()
    {
        var dequeuedDomainEvents = _domainEventList.ToArray();
        _domainEventList.Clear();
        return dequeuedDomainEvents;
    }
}