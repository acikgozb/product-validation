using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult AddProduct()
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
