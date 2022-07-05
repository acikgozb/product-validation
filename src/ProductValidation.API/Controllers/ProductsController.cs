using Microsoft.AspNetCore.Mvc;
using ProductValidation.Core.Dtos;

namespace ProductValidation.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
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
            throw new NotImplementedException();
        }
        
    }
}
