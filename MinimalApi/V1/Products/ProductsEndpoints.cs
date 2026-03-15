using Application.Orchestrators;
using MinimalApi.Extensions;
using MinimalApi.V1.Products.Contracts;

namespace MinimalApi.V1.Products;
public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/v1/products")
            .WithTags("Products");

        group.MapPost("/", AddProduct)
            .WithName("AddProduct")
            .Produces<AddProductResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return group;
    }

    private static async Task<IResult> AddProduct(
    AddProductRequest request,
    IProductOrchestrator orchestrator,
    CancellationToken cancellationToken)
    {
        var result = await orchestrator.AddProduct(request.ToDomain(), cancellationToken);

        var apiResponse = result.ToApiResponse(AddProductResponse.FromDomain);
        return apiResponse;
    }
}