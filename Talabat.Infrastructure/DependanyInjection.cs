using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Infrastructure;
using Talabat.Infrastructure.Basket_Repository;

namespace Talabat.Infrastructure
{
    public static class DependanyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton(typeof(IConnectionMultiplexer), serviceProvider =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                var connectionMultiplexerObj=ConnectionMultiplexer.Connect(connectionString!);
                return connectionMultiplexerObj;
            });
            services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            return services;
        }
    }
}
