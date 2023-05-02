using Ecommerce_Website.Dto;
using Ecommerce_Website.Models;

namespace Ecommerce_Website.Services
{
    public interface ICartItemService
    {
        public Task<CartItem> DeleteCartItem(int id);
        public Task<Order> UpdateCartItem(CartItem cartitem);
    }
}
