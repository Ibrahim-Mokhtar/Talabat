using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using Talabat.Infrastructure.Presistence._Identity;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Infrastructure.Presistence.Data.Interceptors;

namespace Talabat.Infrastructure.Presistence
{
    public static class DependanyInjection
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Store DbContext
            services.AddDbContext<StoreDbContext>((optionBuilder) =>
               {
                   optionBuilder.UseLazyLoadingProxies()
                   .UseSqlServer(configuration.GetConnectionString("StoreContext"));
               });
            services.AddScoped(typeof(IStoreDbtInitializer), typeof(StoreDbInitializer));
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptors));
            #endregion

            #region Identity DbContext
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
