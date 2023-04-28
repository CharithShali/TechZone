using Ecommerce_Website.Dto;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Website.Services
{
    public interface IOrderService
    {
        public Task<Order> AddOrder(Order orderObj);
        public  Task<List<Order>> OrderList();
        public Task <Order> OrderEdit(EditOrderDto editorder);
        public Task<List<Order>> OrderListbyId(int id);
    }
}
