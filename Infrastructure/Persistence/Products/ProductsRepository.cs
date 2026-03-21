using Application.Repositories;
using Common.Errors;
using Common.Results;
using Domain;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Product;
using Logging;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductsRepository : IProductRepository
{
    private readonly Repository<ProductsDbModel> _repo;
    private readonly IAppLogger<ProductsRepository> _logger;

    public ProductsRepository(AppDbContext context, IAppLogger<ProductsRepository> logger)
    {
        _repo = new Repository<ProductsDbModel>(context);
        _logger = logger;
    }

    public async Task<Result<Domain.Product>> GetById(HasProductId id, CancellationToken cancellationToken)
    {
        var entity = await _repo.FindOneAsync(p => p.Id == id.Value);

        if (entity is null)
        {
            return new(
                new NotFoundError([$"Product not found. ProductId: {id}."], new NoException())
            );
        }

        return entity.ToDomain();
    }

    public async Task<Result<Domain.Product>> Add(Domain.Product product, CancellationToken cancellationToken)
    {
        try
        {
            var entity = ProductsDbModel.FromDomain(product);

            await _repo.AddAsync(entity);

            return entity.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.Error($"Error while persisting product. ProductId: {product.Id}", ex);

            return new(
                new UnexpectedError([ex.Message],new HasException(ex))
            );
        }
    }

    public async Task<Result<List<Domain.Product>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _repo.GetAll().ToListAsync();
            var products = entities.Select(e => e.ToDomain().Value).ToList();
            return new(products);
        }
        catch (Exception ex)
        {
            _logger.Error("Error while fetching all products", ex);

            return new(
                new UnexpectedError([ex.Message], new HasException(ex))
            );
        }
    }
}