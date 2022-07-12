using Microsoft.AspNetCore.Mvc;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductValidationService _productValidationService;

    public ProductsController(IProductService productService, IProductValidationService productValidationService)
    {
        _productService = productService;
        _productValidationService = productValidationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> AddProduct(ProductRequestDto productRequestDto)
    {
        var validationResult = _productValidationService.ValidateProduct(productRequestDto);
        if (validationResult.Count > 0)
        {
            return BadRequest(validationResult);
        }

        var response = await _productService.AddProductAsync(productRequestDto);

        //TODO acikgozb: add proper URI once product read endpoint is implemented.
        return response.Match<IActionResult>(
            serviceValidationResult => BadRequest(serviceValidationResult),
            addedProduct => CreatedAtAction(nameof(GetProductById), new {Id = addedProduct.Id}, addedProduct)
        );
    }
}