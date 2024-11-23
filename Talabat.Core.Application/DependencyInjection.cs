using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Orders;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Mapping;
using Talabat.Core.Application.Services;
using Talabat.Core.Application.Services.Basket;
using Talabat.Core.Application.Services.Orders;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contracts.Infrastructure;

namespace Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(AssembleyInformation).Assembly);
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            //services.AddScoped(typeof(Func<IBasketServices>), typeof(Func<BasketServices>));

            services.AddScoped(typeof(IBasketServices), typeof(BasketServices));
            services.AddScoped(typeof(Func<IBasketServices>), serviceProvider =>
            {
                //var mapper = serviceProvider.GetRequiredService<IMapper>();
                //var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                //var basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();

                //return () => new BasketServices(basketRepository, mapper, configuration);

                return ()=>serviceProvider.GetRequiredService<IBasketServices>();
            });

            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(Func<IOrderService>), serviceProvider =>
            {
                 return () => serviceProvider.GetRequiredService<IOrderService>();
            });
            return services;
        }
    }
}
