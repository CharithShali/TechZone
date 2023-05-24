using Ecommerce_Website.Context;
using Ecommerce_Website.Dto;
using Ecommerce_Website.Models;
using Ecommerce_Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly TechZoneContext _authContext;
        private readonly IOrderService _orderService;

        public OrderController(TechZoneContext authContext, IOrderService orderService)
        {
            _authContext = authContext;
            _orderService = orderService;
        }

        [HttpPost("addOrder")]
        public async Task<IActionResult> AddOrder([FromBody] Order orderObj)
        {

            if (orderObj == null)
                return BadRequest();
          var orde=await _orderService.AddOrder(orderObj);
            if (orde == null) { return BadRequest(); }
            else
            {
                return Ok(new
                {
                    message = "Ordered!"
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders  =await _orderService.OrderList();
            return Ok(orders);


        }
        [HttpPut]
        public async Task<IActionResult> Order(EditOrderDto editorder)
        {
            var order=await _orderService.OrderEdit(editorder);
            if (order == null)
            {
                return BadRequest();
            }
            else {
                return Ok(order);
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Order([FromRoute]int id)
        {
            var order = await _orderService.OrderListbyId(id);
            return Ok(order);





        }




    }
}
