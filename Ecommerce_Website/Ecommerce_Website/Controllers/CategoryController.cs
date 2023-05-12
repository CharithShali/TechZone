
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
    public class CategoryController : ControllerBase
    {
        private readonly TechZoneContext _authContext;
        private readonly ICategoryService _categoryService;

        public CategoryController(TechZoneContext authContext,ICategoryService categoryService)
        {
            _authContext = authContext;
            _categoryService = categoryService;
        }

        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategory([FromBody] ProductCategory catObj)
        {

            if (catObj == null)
            return BadRequest();
          var category=await _categoryService.Add(catObj);
            return Ok(new
            {
                message = "User Registed!",
                category = category
            }); ;
        }

        [HttpGet]
        public async Task<IActionResult> ViewCat()
        {
            var cat =await _categoryService.GetAll();
            return Ok(cat);


        }
    
    }
}
