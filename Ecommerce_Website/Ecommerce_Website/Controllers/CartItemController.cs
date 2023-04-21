using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpDelete("delete")]
        public async Task<IActionResult> RegisterAcart(int id)
        {

            var cartitem=await _authContext.CartItems.FindAsync(id);
            _authContext.CartItems.Remove(cartitem);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                message = "Cart item Deleted!"
            });


        }
        [HttpPut]
        public async Task<IActionResult> Update(CartItem cardo)
        {
            CartItem? cartitem = await _authContext.CartItems.FindAsync(cardo.CartItemId);
            if (cardo == null)
            {
                return BadRequest("Hero Not Found");
            }
           cartitem.Quantity= cardo.Quantity;

            await _authContext.SaveChangesAsync();

            return Ok(await _authContext.CartItems.ToListAsync());
        }




    }
}
