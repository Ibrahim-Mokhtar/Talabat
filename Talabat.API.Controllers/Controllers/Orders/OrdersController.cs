using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
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
    }
}
