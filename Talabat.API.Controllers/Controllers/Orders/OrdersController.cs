using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Models.Common;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.API.Controllers.Controllers.Orders
{
    public class OrdersController(IServiceManager serviceManager):BaseApiController
    {
        [HttpPost] // POST: /api/Orders
        public  async Task<ActionResult<OrderToCreateDto>> CreateOrder(OrderToCreateDto order)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.CreateOrderAsync(buyerEmail!,order);
            return Ok(result);
        }
        [HttpGet] // GET: /api/Orders
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result=await serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail!);
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: /api/Orders/id
        public async Task<ActionResult<OrderToReturnDto>>GetOrderById(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!,id);
            return Ok(result);
        }

        [HttpGet("deliveryMethods")] // GET: /api/Orders/deliveryMethods
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var result=await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(result);
        }
    }
}
