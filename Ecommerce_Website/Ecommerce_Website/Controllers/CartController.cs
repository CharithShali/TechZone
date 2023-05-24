
using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly TechZoneContext _authContext;

        public CartController(TechZoneContext authContext)
        {
            _authContext = authContext;
        }



        [HttpPost("Cart")]
        public async Task<IActionResult> RegisterAcart([FromBody] Cart cartObj)
        {

            if (cartObj == null)
                return BadRequest();
            cartObj.Ordered = "Pending";
            await _authContext.Carts.AddAsync(cartObj);
            await _authContext.SaveChangesAsync();
            var carts=await _authContext.Carts.ToListAsync();
            var cart = carts.Last();
            return Ok(cart.CartId);


        }

        [HttpGet]
        public async Task<IActionResult> ViewCarts()
        {
            var carts = await _authContext.Carts.ToListAsync();
            return Ok(carts);


        }
        [HttpPut]
        public async Task<IActionResult> Update(Cart cardo)
        {
            Cart? cart = await _authContext.Carts.FindAsync(cardo.CartId);
            if (cardo == null)
            {
                return BadRequest("Hero Not Found");
            }
            cart.Ordered = "Ordered";
         
            await _authContext.SaveChangesAsync();
            return Ok(await _authContext.Carts.ToListAsync());
        }

      
        [HttpGet("Cart")]
        public async Task<IActionResult> ShowCartDetails(int id)
        {
            var fullEntries = _authContext.Carts
         .Join(
             _authContext.CartItems,
             cart => cart.CartId,
               cartitem => cartitem.CartId,
             (cart, cartitem) => new { cart, cartitem }
         )
         .Join(
            _authContext.Products,
             combinedEntry => combinedEntry.cartitem.ProductId,
             product => product.ProductId,
             (combinedEntry, product) => new
             {
                productimage = product.ImageName,
                 productname = product.Title,
                 cartquantity= combinedEntry.cartitem.Quantity,
                 productprice=product.Price,
                 amount= product.Price * combinedEntry.cartitem.Quantity,
                 userid=combinedEntry.cart.UserId,
                 cartitemid=combinedEntry.cartitem.CartItemId
             }
         )
         .Where(fullEntry => fullEntry.userid ==id)
         .Take(10);


            return Ok(fullEntries);


        }

    }
}