using Microsoft.AspNetCore.Mvc;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Models;
using ProductValidation.Core.Models.Dtos;

namespace ProductValidation.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IModelValidator _modelValidator;

    public ProductsController(IModelValidator modelValidator)
    {
        _modelValidator = modelValidator;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        //TODO: Implement this endpoint after POST endpoint + DB implementation
        throw new NotImplementedException();
    }

    [HttpPost]
    [Consumes("application/json")]
    public IActionResult AddProduct(ProductRequestDto productRequestDto)
    {
        var validationResult = _modelValidator.Validate(productRequestDto);
        if (!validationResult.IsValid)
        {
            var fieldValidationResult = validationResult.Errors.ConvertAll(validationFailure =>
                new FieldValidationResult
                {
                    FieldName = validationFailure.PropertyName,
                    ErrorMessage = validationFailure.ErrorMessage
                });

            return BadRequest(fieldValidationResult);
        }
        
        //Valid DTO model -> call service to handle business logic.

        throw new NotImplementedException();
    }
}