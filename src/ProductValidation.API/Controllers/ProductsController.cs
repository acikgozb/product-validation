using Microsoft.AspNetCore.Mvc;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;
using ProductValidation.Core.Services;

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
    public IActionResult GetProducts()
    {
        //TODO: Implement this endpoint after POST endpoint + DB implementation
        throw new NotImplementedException();
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

        await _productService.AddProductAsync(productRequestDto);
        
        throw new NotImplementedException();
    }
}