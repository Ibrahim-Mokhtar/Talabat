using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Infrastructure.Presistence.Data.Interceptors;

namespace Talabat.Infrastructure.Presistence
{
    public static class DependanyInjection
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>((optionBuilder) =>
            {
                optionBuilder.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });
            services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptors));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            return services;
        }
    }
}
