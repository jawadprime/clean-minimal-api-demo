using Application.Repositories;
using Common.Results;
using Domain;
using Logging;

namespace Application.Orchestrators;

public interface IProductOrchestrator 
{
    public Task<Result<Product>> AddProduct(Product product, CancellationToken cancellationToken);
    public Task<Result<Product>> GetProductById(HasProductId id, CancellationToken cancellationToken);
}

public class ProductOrchestrator : IProductOrchestrator
{
    private readonly IProductRepository _productRepo;
    private readonly IAppLogger<ProductOrchestrator> _logger;

    public ProductOrchestrator(IProductRepository repository, IAppLogger<ProductOrchestrator> logger)
    {
        _productRepo = repository;
        _logger = logger;
    }

    public async Task<Result<Product>> GetProductById(HasProductId id, CancellationToken ct)
    {
        var product = await _productRepo.GetById(id, ct);

        return product;
    }

    public async Task<Result<Product>> AddProduct(Product product, CancellationToken ct)
    {
        var addProductResult = await _productRepo.Add(product, ct);

        if (addProductResult.IsSuccess)
            _logger.Information($"Product successfuly added. ProductId: {addProductResult.Value.Id}");

        return addProductResult;
    }
}