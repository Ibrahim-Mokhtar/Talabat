using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Mapping;
using Talabat.Core.Application.Services;

namespace Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(AssembleyInformation).Assembly);
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
           
            return services;
        }
    }
}
