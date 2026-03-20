using Domain;
using Domain.Validators;

namespace MinimalApi.V1.Products.Contracts;

public record AddProductRequest(string Name) 
{
    public ProductArguments ToDomainArguments() 
    {
        return new(new NoProductId(), new(Name), new NoProductCreatedAt());
    }
}

public record AddProductResponse(Guid Id, string Name, DateTime CreatedAt)
{
    public static AddProductResponse FromDomain(Product product)
    {
        return new(
            ((HasProductId)product.Id).Value,
            product.Name.Value,
            ((HasProductCreatedAt)product.CreatedAt).Value);
    }
}