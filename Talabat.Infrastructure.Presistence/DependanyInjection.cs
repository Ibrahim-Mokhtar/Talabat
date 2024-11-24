using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Infrastructure.Presistence.Data.Interceptors;
using Talabat.Infrastructure.Presistence.Identity;

namespace Talabat.Infrastructure.Presistence
{
    public static class DependanyInjection
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Store Context
            services.AddDbContext<StoreDbContext>((serviceProvider,optionBuilder) =>
                {
                    optionBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("StoreContext"))
                    .AddInterceptors(serviceProvider.GetRequiredService<CustomSaveChangesInterceptors>());
                });
            services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));
            services.AddScoped(typeof(CustomSaveChangesInterceptors));
            #endregion

            #region Identity Context
            services.AddDbContext<StoreIdentityDbContext>((optionBuilder) =>
            {
                optionBuilder.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            });

            services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));
            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            return services;
        }
    }
}
