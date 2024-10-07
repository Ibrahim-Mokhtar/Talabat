using Talabat.Core.Domain.Contracts;

namespace Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializerStoreContextAsync(this WebApplication app)
        {
            var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
            // Ask Runtime Env an Object from "StoreContext" Services Explicitly 

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //var logger = services.GetRequiredService<ILogger<Program>>();
            try
            {
                await storeContextInitializer.InitalizeAsync();
                await storeContextInitializer.SeedAsync();
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
