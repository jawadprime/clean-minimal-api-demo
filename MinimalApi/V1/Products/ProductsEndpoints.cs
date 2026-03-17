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

        group.MapGet("/{id}", GetProductById)
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/Add", AddProduct)
            .WithName("AddProduct")
            .Produces<AddProductResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

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

    private static async Task<IResult> GetProductById(
        Guid id,
        IProductOrchestrator orchestrator,
        CancellationToken cancellationToken)
    {
        var result = await orchestrator.GetProductById(id, cancellationToken);

        var apiResponse = result.ToApiResponse(GetProductByIdResponse.FromDomain);
        return apiResponse;
    }
}