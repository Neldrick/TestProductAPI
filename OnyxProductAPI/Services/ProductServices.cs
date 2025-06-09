using OnyxProductAPI.Models;
using OnyxProductAPI.Repositories;

namespace OnyxProductAPI.Services;

public class ProductServices : IProductServices
{
    private readonly IProductRepository _productRepository;
    public ProductServices(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product> CreateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        return await _productRepository.CreateProductAsync(product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsByColorAsync(string color)
    {
        return await _productRepository.GetAllProductsByColorAsync(color);
    }
}