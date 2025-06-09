using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnyxProductAPI.Models;
using OnyxProductAPI.Services;
using OnyxProductAPI.Utils.Authen;

namespace OnyxProductAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize(AuthenticationSchemes = ApiKeyAuthenticationSchemeOption.DefaultScheme)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController : ControllerBase
{
    private readonly IProductServices _productServices;

    public ProductController(IProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var products = await _productServices.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest("Product cannot be null");
        }

        var createdProduct = await _productServices.CreateProductAsync(product);
        var result = CreatedAtAction(nameof(GetAllProductsAsync), new { id = createdProduct.Id }, createdProduct);
        return Ok(result);
    }

    [HttpGet("{color}")]
    public async Task<IActionResult> GetAllProductsByColorAsync(string color)
    {
        var products = await _productServices.GetAllProductsByColorAsync(color);
        return Ok(products);
    }
}