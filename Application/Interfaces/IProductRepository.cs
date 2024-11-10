namespace Application.Interfaces;

using Domain.Entities;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task DeleteAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetHistoryAsync(int id);
    Task UpdateAsync(Product product);
}
