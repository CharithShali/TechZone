using Azure;
using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
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

        public ProductController(TechZoneContext authContext)
        {
            _authContext = authContext;
        }


        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product proObj)
        {
            var cate = _authContext.ProductCategories.Find(proObj.CategoryId);
            var user=_authContext.Users.Find(1);

            Product product = new Product();

            product.ProductId = proObj.ProductId;
            product.ImageName = proObj.ImageName;
            product.Description = proObj.Description;
            product.Price = proObj.Price;
            product.Category = cate;
            product.Quantity = proObj.Quantity;
            product.Title = proObj.Title;
            product.User = user;

            if (proObj == null)
                return BadRequest();

            Console.WriteLine(proObj);
            var cat = await _authContext.ProductCategories.ToArrayAsync();
            proObj.Category = cat[0];
 
            await _authContext.Products.AddAsync(product);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                message = "Product Registed!"
            });
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
            var product = _authContext.Products.Where(c => c.ProductId == id).SingleOrDefault();

            return Ok(product);
        }

    }
}
