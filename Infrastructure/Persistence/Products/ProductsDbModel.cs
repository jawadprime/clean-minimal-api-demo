using Common.Results;
using Domain;

namespace Infrastructure.Persistence.Product;
internal record ProductsDbModel (Guid Id, string Name, DateTime CreatedAt)
{
    public Result<Domain.Product> ToDomain() 
        => Domain.Product.Create(new(new HasProductId(Id), new(Name), new HasProductCreatedAt(CreatedAt)));

    public static ProductsDbModel FromDomain(Domain.Product domain)
        => new ProductsDbModel(new Guid(), domain.Name.Value, DateTime.UtcNow);
}