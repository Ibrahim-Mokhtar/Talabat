using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Domain.Contracts;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Infrastructure.Presistence.Data.Interceptors;

namespace Talabat.Infrastructure.Presistence
{
    public static class DependanyInjection
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>((optionBuilder) =>
            {
                optionBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });
            services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableEntityInterceptor));
            return services;
        }   
    }
}
