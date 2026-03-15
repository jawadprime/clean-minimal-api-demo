namespace Infrastructure.Persistence.Product;
internal record ProductEntity (Guid Id, string Name, DateTime CreatedAt)
{
    public Domain.Product ToDomain() => new Domain.Product(Id, Name, CreatedAt);

    public static ProductEntity FromDomain(Domain.Product domain)
        => new ProductEntity(domain.Id, domain.Name, domain.CreatedAt);
}