using Ecommerce_Website.Context;
using Ecommerce_Website.Dto;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Services
{
    public class OrderService : IOrderService
    {
        private readonly TechZoneContext _context;

        public OrderService(TechZoneContext techZoneContext)
        {
            _context = techZoneContext;
        }
        public async Task<Order> AddOrder(Order orderObj)
        {
            await _context.Orders.AddAsync(orderObj);
            await _context.SaveChangesAsync();
            return orderObj;
        }

        public async Task<Order> OrderEdit(EditOrderDto editorder)
        {
            var order = await _context.Orders.FindAsync(editorder.orderid);
            if (editorder.status)
            {
                order.Status = "Accepted";
            }
            else
            {
                order.Status = "Declined";
            }
            order.AgentEmail = editorder.agentEmail;
            await _context.SaveChangesAsync();
            return order;
            
        }

        public async Task<List<Order>> OrderList()
        {

            var Orders=await _context.Orders.ToListAsync();
            return Orders;
        }
        public async Task<List<Order>> OrderListbyId(int id)
        {
          
            var Orders =await _context.Orders.Where(c => c.UserId == id).ToListAsync();
            return Orders;
        }

    }
}
