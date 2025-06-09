using Xunit;
using OnyxProductAPI.Repositories;
using OnyxProductAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxProductAPI.Test;

public class ProductRepositoryTests
{
    private readonly ProductRepository _repository;

    public ProductRepositoryTests()
    {
        _repository = new ProductRepository();
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsAllProducts()
    {
        // Act
        var result = await _repository.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Fact]
    public async Task CreateProductAsync_AddsProductToRepository()
    {
        // Arrange
        var product = new Product { Name = "Monitor", Color = "Black", Price = 199.99M };

        // Act
        var createdProduct = await _repository.CreateProductAsync(product);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.Equal(product.Name, createdProduct.Name);
        Assert.Equal(product.Color, createdProduct.Color);
        Assert.Equal(product.Price, createdProduct.Price);
    }

    [Fact]
    public async Task GetAllProductsByColorAsync_ReturnsFilteredProducts()
    {
        // Act
        var result = await _repository.GetAllProductsByColorAsync("Black");

        // Assert
        Assert.NotNull(result);
        Assert.All(result, p => Assert.Equal("Black", p.Color));
    }
}
