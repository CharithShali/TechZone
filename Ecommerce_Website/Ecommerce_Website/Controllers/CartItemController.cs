using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : Controller
    {
        private readonly TechZoneContext _authContext;

        public CartItemController(TechZoneContext authContext)
        {
            _authContext = authContext;
        }

        [HttpPost("Cartitem")]
        public async Task<IActionResult> RegisterAcart([FromBody] CartItem cartObj)
        {

            if (cartObj == null)
                return BadRequest();
            
            await _authContext.CartItems.AddAsync(cartObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                message = "Cart item Registed!"
            });


        }




    }
}
