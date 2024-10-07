using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Application.Mapping;
using Talabat.Core.Application.Services;

namespace Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            return services;
        }
    }
}
