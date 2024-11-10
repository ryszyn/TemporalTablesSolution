namespace Application.Interfaces;

using Domain.Entities;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);

    Task<Product?> GetAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetHistoryAsync(int id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
}
