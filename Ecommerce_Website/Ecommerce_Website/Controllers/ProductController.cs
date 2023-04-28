using Azure;
using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Ecommerce_Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly TechZoneContext _authContext;
        private readonly IProductService _productService;

        public ProductController(TechZoneContext authContext,IProductService productService)
        {
            _authContext = authContext;
            _productService = productService;
        }


        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product proObj)
        {
           
            if (proObj == null)
                return BadRequest();

            var pro = await _productService.Add(proObj);
            if (pro == null)
            {
                return BadRequest();
            }
            else {
                return Ok(new
                {
                    message = "Product Added!"
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewProducts()
        {

            var products = await _authContext.Products.ToListAsync();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewProduct([FromRoute] int id)
        {
            var product = await _productService.GetById(id);

            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct([FromRoute] int id,Product product)
        {
          var prod= await _productService.Edit(id, product);
            return Ok(prod);
        }

    }
}
