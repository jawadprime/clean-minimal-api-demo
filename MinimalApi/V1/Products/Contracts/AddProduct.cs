using Domain;

namespace MinimalApi.V1.Products.Contracts;

public record AddProductRequest(string Name) 
{
    public Product ToDomain() 
    {
        return new(Guid.NewGuid(), Name, DateTime.UtcNow);
    }
}

public record AddProductResponse(Guid Id, string Name, DateTime CreatedAt)
{
    public static AddProductResponse FromDomain(Product product)
    {
        return new(product.Id, product.Name, product.CreatedAt);
    }
}