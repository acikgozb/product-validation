using Microsoft.AspNetCore.Mvc;
using ProductValidation.Core.Contracts;
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


    //If validation passes, map DTO to entity and save it to DB.
    [HttpPost]
    [Consumes("application/json")]
    public IActionResult AddProduct(ProductRequestDto productRequestDto)
    {
        throw new NotImplementedException();
    }
}