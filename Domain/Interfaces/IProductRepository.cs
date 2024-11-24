namespace Domain.Interfaces;

using Domain.Entities;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);

    Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetHistoryAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
}
