namespace Infrastructure.Persistence.Common;

public abstract class BaseEntity();
public abstract class BaseEntity<TKey> : BaseEntity
{
    public TKey Id { get; set; } = default!;
}