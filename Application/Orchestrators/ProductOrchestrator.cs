using Application.Repositories;
using Common.Results;
using Domain;

namespace Application.Orchestrators;

public interface IProductOrchestrator 
{
    public Task<Result<Product>> AddProduct(Product product, CancellationToken cancellationToken);
    public Task<Result<Product>> GetProductById(Guid id, CancellationToken cancellationToken);
}

public class ProductOrchestrator : IProductOrchestrator
{
    private readonly IProductRepository _productRepo;

    public ProductOrchestrator(IProductRepository repository)
    {
        _productRepo = repository;
    }

    public async Task<Result<Product>> GetProductById(Guid id, CancellationToken ct)
    {
        var product = await _productRepo.GetById(id, ct);

        return product;
    }

    public async Task<Result<Product>> AddProduct(Product product, CancellationToken ct)
    {
        var createdProduct = await _productRepo.Add(product, ct);

        return createdProduct;
    }
}