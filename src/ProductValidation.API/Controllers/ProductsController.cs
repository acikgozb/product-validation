using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.API.Controllers;

[ApiVersion("1.0")]
[Route("api/products")]
[ApiController]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductValidationService _productValidationService;

    public ProductsController(IProductService productService, IProductValidationService productValidationService)
    {
        _productService = productService;
        _productValidationService = productValidationService;
    }

    /// <summary>
    /// Returns a list of all added products.
    /// </summary>
    /// <returns>A list of all added products.</returns>
    /// <response code="200">List of products - <c>200 Ok</c>.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Returns a single product by its Id.
    /// </summary>
    /// <param name="Id">Target Product's Id.</param>
    /// <returns>Corresponding product for given Id.</returns>
    /// <response code="200">If product is found - <c>200 Ok</c>. </response>
    /// <response code="404">If there is no product that has requested Id - <c>404 NotFound</c></response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    /// <summary>
    /// Allows client to add user-submitted Product.
    /// </summary>
    /// <param name="productRequestDto"><c>ProductRequestDto</c> - A product that is submitted by user.</param>
    /// <remarks>
    /// If a full <c>ProductRequestDto</c> is not provided, there will be validation errors.<br/>
    /// This endpoint is only for saving the entire user-submitted product to DB.<br/>
    /// It is not possible to do a partial update.<br/>
    /// </remarks>
    /// <returns>
    /// Corresponding validation errors if any field is not valid.<br />
    /// Added product entity if fields are valid.
    /// </returns>
    /// <response code="201">If all fields are valid - <c>201 Created.</c></response>
    /// <response code="400"> If any field is invalid - <c>400 BadRequest.</c></response>
    [HttpPost]
    [ProducesResponseType(typeof(List<FieldValidationResult>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddProduct(ProductRequestDto productRequestDto)
    {
        var validationResult = _productValidationService.ValidateProduct(productRequestDto);
        if (validationResult.Count > 0)
        {
            return BadRequest(validationResult);
        }

        var response = await _productService.AddProductAsync(productRequestDto);

        return response.Match<IActionResult>(
            serviceValidationResult => BadRequest(serviceValidationResult),
            addedProduct => CreatedAtAction(nameof(GetProductById), new { Id = addedProduct.Id }, addedProduct)
        );
    }
}