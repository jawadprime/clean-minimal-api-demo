using Application.Repositories;
using Common.Errors;
using Common.Results;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly Repository<ProductEntity> _repo;

    public ProductRepository(AppDbContext context)
    {
        _repo = new Repository<ProductEntity>(context);
    }

    public async Task<Result<Domain.Product>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repo.FindOneAsync(p => p.Id == id);

        if (entity is null)
        {
            return new(
                new NotFoundError([$"Product not found with Id : {id}"], new NoException())
            );
        }

        return new(entity.ToDomain());
    }

    public async Task<Result<Domain.Product>> Add(Domain.Product product, CancellationToken cancellationToken)
    {
        try
        {
            var entity = ProductEntity.FromDomain(product);

            await _repo.AddAsync(entity);

            return new(entity.ToDomain());
        }
        catch (Exception ex)
        {
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
            var products = entities.Select(e => e.ToDomain()).ToList();
            return new(products);
        }
        catch (Exception ex)
        {
            return new(
                new UnexpectedError([ex.Message], new HasException(ex))
            );
        }
    }
}