using Common.Results;
using Domain.Validators;

namespace Domain;

public record Product
{
    public ProductId Id { get; init; }
    public ProductName Name { get; init; }
    public ProductCreatedAt CreatedAt { get; init; }

    private Product(ProductId id, ProductName name, ProductCreatedAt createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }

    public static Result<Product> Create(ProductArguments arguments) 
    {
        Result validationResult = ProductValidator.Validate(arguments);

        if (validationResult.IsFailure)
            return new(validationResult.Error);

        return new(new Product(arguments.Id, arguments.Name, arguments.CreatedAt));
    }
}