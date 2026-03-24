using Common.Results;
using Domain;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Product;
internal class ProductsDbModel : BaseEntity<Guid>
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public ProductsDbModel() { }

    public ProductsDbModel(Guid id, string name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }

    public Result<Domain.Product> ToDomain() 
        => Domain.Product.Create(new(new HasProductId(Id), new(Name), new HasProductCreatedAt(CreatedAt)));

    public static ProductsDbModel FromDomain(Domain.Product domain)
        => new ProductsDbModel(new Guid(), domain.Name.Value, DateTime.UtcNow);
}