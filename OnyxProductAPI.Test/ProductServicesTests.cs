using Xunit;
using Moq;
using OnyxProductAPI.Services;
using OnyxProductAPI.Repositories;
using OnyxProductAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnyxProductAPI.Test;

public class ProductServicesTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ProductServices _service;

    public ProductServicesTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _service = new ProductServices(_mockProductRepository.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Laptop", Color = "Silver", Price = 999.99M } };
        _mockProductRepository.Setup(r => r.GetAllProductsAsync()).ReturnsAsync(products);

        // Act
        var result = await _service.GetAllProductsAsync();

        // Assert
        Assert.Equal(products, result);
    }

    [Fact]
    public async Task CreateProductAsync_ThrowsException_WhenProductIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateProductAsync(null));
    }

    [Fact]
    public async Task CreateProductAsync_ReturnsCreatedProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop", Color = "Silver", Price = 999.99M };
        _mockProductRepository.Setup(r => r.CreateProductAsync(product)).ReturnsAsync(product);

        // Act
        var result = await _service.CreateProductAsync(product);

        // Assert
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task GetAllProductsByColorAsync_ReturnsFilteredProducts()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Laptop", Color = "Silver", Price = 999.99M } };
        _mockProductRepository.Setup(r => r.GetAllProductsByColorAsync("Silver")).ReturnsAsync(products);

        // Act
        var result = await _service.GetAllProductsByColorAsync("Silver");

        // Assert
        Assert.Equal(products, result);
    }
}
