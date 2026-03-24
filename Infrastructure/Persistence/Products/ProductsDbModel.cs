using Common.Results;
using Domain;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Product;
internal class ProductsDbModel : BaseEntity<Guid>
{
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }

    public Result<Domain.Product> ToDomain() 
        => Domain.Product.Create(new(new HasProductId(Id), new(Name), new HasProductCreatedAt(CreatedAt)));

    public static ProductsDbModel FromDomain(Domain.Product domain)
        => new() { Id = new Guid(), Name = domain.Name.Value, CreatedAt = DateTime.UtcNow };
}