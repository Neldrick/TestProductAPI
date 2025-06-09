using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OnyxProductAPI.Controllers;
using OnyxProductAPI.Services;
using OnyxProductAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace OnyxProductAPI.Test;

public class ProductControllerTests
{
    private readonly Mock<IProductServices> _mockProductServices;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductServices = new Mock<IProductServices>();
        _controller = new ProductController(_mockProductServices.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsOkResult()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Laptop", Color = "Silver", Price = 999.99M } };
        _mockProductServices.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetAllProductsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(products, okResult.Value);
    }

    [Fact]
    public async Task CreateProductAsync_ReturnsBadRequest_WhenProductIsNull()
    {
        // Act
        var result = await _controller.CreateProductAsync(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateProductAsync_ReturnsCreatedProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop", Color = "Silver", Price = 999.99M };
        _mockProductServices.Setup(s => s.CreateProductAsync(product)).ReturnsAsync(product);

        // Act
        var result = await _controller.CreateProductAsync(product);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(product.Name, returnedProduct.Name);
    }

    [Fact]
    public async Task GetAllProductsByColorAsync_ReturnsFilteredProducts()
    {
        // Arrange
        var products = new List<Product> { new Product { Id = 1, Name = "Laptop", Color = "Silver", Price = 999.99M } };
        _mockProductServices.Setup(s => s.GetAllProductsByColorAsync("Silver")).ReturnsAsync(products);

        // Act
        var result = await _controller.GetAllProductsByColorAsync("Silver");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(products, okResult.Value);
    }
}
