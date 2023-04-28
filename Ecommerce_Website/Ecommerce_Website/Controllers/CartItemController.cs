
using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Crm.Sdk.Messages;
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

        [HttpPost("{id}")]
        public async Task<IActionResult> RegisterAcart([FromBody] CartItem cartObj, [FromRoute] int id)
        {

            if (cartObj == null)
                return BadRequest();

            var Product = _authContext.Products.Find(cartObj.ProductId);

            Product.Quantity = Product.Quantity - cartObj.Quantity;
            await _authContext.SaveChangesAsync();
            
            var user = (
              from ai in _authContext.Carts
              join al in _authContext.CartItems on ai.CartId equals al.CartId
              where (ai.UserId == id && al.ProductId==cartObj.ProductId)
              select new AppInformation
              {
                  NavigationType=ai.CartId,

              }).FirstOrDefault();


            if (user==null)
            {
                await _authContext.CartItems.AddAsync(cartObj);
                await _authContext.SaveChangesAsync();       
               
            }
            else {
                var CartItem = _authContext.CartItems.Where(c => c.CartId == user.NavigationType).SingleOrDefault();
                    CartItem.Quantity = CartItem.Quantity + cartObj.Quantity;
                    await _authContext.SaveChangesAsync();

            };

            return Ok(new
            {
                message = "Product Registed!"
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
