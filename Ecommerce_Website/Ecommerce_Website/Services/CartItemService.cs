using Ecommerce_Website.Context;
using Ecommerce_Website.Models;

namespace Ecommerce_Website.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly TechZoneContext _context;

        public CartItemService(TechZoneContext techZoneContext)
        {
            _context = techZoneContext;
        }

        public Task<CartItem> DeleteCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateCartItem(CartItem cartitem)
        {
            throw new NotImplementedException();
        }
    }
}
