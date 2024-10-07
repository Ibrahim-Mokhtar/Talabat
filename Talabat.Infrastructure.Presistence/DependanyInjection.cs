using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Domain.Contracts;
using Talabat.Infrastructure.Presistence.Data;

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
            return services;
        }   
    }
}
