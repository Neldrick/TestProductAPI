using OnyxProductAPI.Models;

namespace OnyxProductAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Color = "Silver", Price = 999.99M },
        new Product { Id = 2, Name = "Smartphone", Color = "Black", Price = 499.99M },
        new Product { Id = 3, Name = "Tablet", Color = "White", Price = 299.99M }
    };

    public async Task<Product?> CreateProductAsync(Product product)
    {
        if (product != null)
        {
            product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return await Task.FromResult(product);
        }
        return null;
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products);
    }

    public Task<IEnumerable<Product>> GetAllProductsByColorAsync(string color)
    {
        var productsByColor = _products.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(productsByColor);
    }

}