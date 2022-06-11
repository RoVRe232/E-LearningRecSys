using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Application.Dtos.Orders;
using OrdersAPI.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrdersAPI.Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [Route("create-order")]
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromQuery] OrderDTO order)
        {
            var orderResult = await _ordersService.CreateOrder(order);
            if (orderResult == true)
                return Ok(order);
            return BadRequest(orderResult);
        }
    }
}
