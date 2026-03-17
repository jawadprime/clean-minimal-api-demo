using Domain;

namespace MinimalApi.V1.Products.Contracts;

public record GetProductByIdResponse(Guid Id, string Name, DateTime CreatedAt)
{
    public static AddProductResponse FromDomain(Product product)
    {
        return new(product.Id, product.Name, product.CreatedAt);
    }
}