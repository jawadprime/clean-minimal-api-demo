using Common.Errors;
using Common.Results;

namespace Domain.Validators;

public record ProductArguments(ProductId Id, ProductName Name, ProductCreatedAt CreatedAt);

public static class ProductValidator
{
    public static Result Validate(ProductArguments argument)
    {
        var failures = new List<string>();

        if (argument.Name.Value.Length > 100)
            failures.Add("Product name cannot exceed 100 characters.");

        return Result.Failure(new ValidationError(failures, new NoException()));
    }
}