using Talabat.APIs.Extensions;
using Talabat.APIs.Services;
using Talabat.Core.Application.Abstraction;
using Talabat.Infrastructure.Presistence;
using Talabat.Core.Application;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {



            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers()
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly); // Register Required Services by ASP.NET Core Web APIs to DI Container


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddPersistanceServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddApplicationServices();

            webApplicationBuilder.Services.AddScoped(typeof(ILogedInUserService), typeof(LogedInUserService));
           

            #endregion

            var app = webApplicationBuilder.Build();

            #region Database Initialization
            await app.InitializerStoreContextAsync();
            #endregion  

            #region Configure Kestrel Middleware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
