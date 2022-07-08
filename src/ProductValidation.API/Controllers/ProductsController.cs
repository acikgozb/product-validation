using Microsoft.AspNetCore.Mvc;
using ProductValidation.Core.Contracts;
using ProductValidation.Core.Dtos;

namespace ProductValidation.API.Controllers
{
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
        public IActionResult AddProduct(ProductRequestDto productRequestDto)
        {
            /*
             * TODO:
             * ProductRequestDto
             * Product Entity
             */
            /*
             * If validation passes, map DTO to entity and save it to DB.
             */
            _modelValidator.Validate(productRequestDto);
            throw new NotImplementedException();
        }
        
    }
}
