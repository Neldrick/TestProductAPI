using OnyxProductAPI.Models;

namespace OnyxProductAPI.Services;

public interface IProductServices
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task<IEnumerable<Product>> GetAllProductsByColorAsync(string color);
}