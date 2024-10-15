using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Common.Exceptions;
using Talabat.Core.Domain.Contracts.Infrastructure;
using Talabat.Core.Domain.Entites.Basket;

namespace Talabat.Core.Application.Services.Basket
{
    internal class BasketServices(IBasketRepository basketRepository, IMapper mapper,IConfiguration configuration) : IBasketServices
    {
        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
        {
            var basket=await basketRepository.GetAsync(basketId);
            if (basket is null) throw new NotFoundException();
            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasket)
        {
            var basket=mapper.Map<CustomerBasket>(customerBasket);
            var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));
            var updatedBasket = await basketRepository.UpdateAsync(basket, timeToLive);
            if (updatedBasket is null) throw new NotFoundException();
            return customerBasket;

        }
        public async Task DeleteCustomerBasketAsync(string basketId)
        {
            var deleted = await basketRepository.DeleteAsync(basketId);
            if(!deleted) throw new NotFoundException();

        }

    }
}
