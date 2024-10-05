
using Microsoft.EntityFrameworkCore;
using Talabat.Infrastructure.Presistence;
using Talabat.Infrastructure.Presistence.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {



            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers(); // Register Required Services by ASP.NET Core Web APIs to DI Container


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddPersistanceServices(webApplicationBuilder.Configuration);

            #endregion

            var app = webApplicationBuilder.Build();
            var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<StoreContext>();
            // Ask Runtime Env an Object from "StoreContext" Services Explicitly 

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //var logger = services.GetRequiredService<ILogger<Program>>();
            try
            {
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if(pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync();
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occurud during applying the migrations.");
            }

            #region Configure Kestrel Middleware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
