using OnyxProductAPI.Models;

namespace OnyxProductAPI.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetAllProductsByColorAsync(string color);
    Task<Product?> CreateProductAsync(Product product);
}