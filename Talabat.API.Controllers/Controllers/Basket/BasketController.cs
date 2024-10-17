using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.API.Controllers.Controllers.Basket
{
    public class BasketController(IServiceManager serviceManager):BaseApiController
    {
        [HttpGet] // GET: /api/Basket?id
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
        {
            var basket=await serviceManager.BasketServices.GetCustomerBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost] // POST: api/Basket
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = await serviceManager.BasketServices.UpdateCustomerBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete] // DELETE: api/Basket
        public async Task DeleteBasket(string id)
        {
            await serviceManager.BasketServices.DeleteCustomerBasketAsync(id);
        }
    }
}
