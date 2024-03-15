namespace Ordering.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public T Id { get; set; } = default!;
}