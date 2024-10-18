using Talabat.Core.Domain.Contracts.Persistence.DbInitializers;

namespace Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {
            var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var storeContextInitializer = services.GetRequiredService<IStoreDbtInitializer>();
            var storeIdentityContextInitializer = services.GetRequiredService<IStoreIdentityDbInitializer>();
            // Ask Runtime Env an Object from "StoreContext" Services Explicitly 

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //var logger = services.GetRequiredService<ILogger<Program>>();
            try
            {
                await storeContextInitializer.InitalizeAsync();
                await storeContextInitializer.SeedAsync();

                await storeIdentityContextInitializer.InitalizeAsync();
                await storeIdentityContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occurud during applying the migrations or the data seeding.");
            }
            return app;
        }

    }
}
