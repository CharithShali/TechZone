
using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly TechZoneContext _authContext;

        public CategoryController(TechZoneContext authContext)
        {
            _authContext = authContext;
        }

        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategory([FromBody] ProductCategory catObj)
        {

            if (catObj == null)
            return BadRequest();
            await _authContext.ProductCategories.AddAsync(catObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                message = "User Registed!"
            });
        }

        [HttpGet]
        public async Task<IActionResult> ViewCat()
        {
            var cat = await _authContext.ProductCategories.ToListAsync();
            return Ok(cat);


        }

    }
}
