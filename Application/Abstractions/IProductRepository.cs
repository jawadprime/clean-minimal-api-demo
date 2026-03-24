using Common.Results;
using Domain;

namespace Application.Repositories;

public interface IProductRepository
{
    Task<Result<Product>> GetById(HasProductId id, CancellationToken cancellationToken);
    Task<Result<Product>> Add(Product product, CancellationToken cancellationToken);
    Task<Result<List<Product>>> GetAll(CancellationToken cancellationToken);
}