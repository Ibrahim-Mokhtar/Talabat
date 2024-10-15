
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Extensions;
using Talabat.APIs.Services;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Domain.Contracts;
using Talabat.Infrastructure.Presistence;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Core.Application;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Controllers.Errors;
using Talabat.APIs.Middlewares;

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
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count() > 0)
                                                            .SelectMany(P => P.Value!.Errors)
                                                            .Select(P => P.ErrorMessage);
                        return new BadRequestObjectResult(new ApiValidationErrorResponse() 
                        {
                        Errors=errors
                        });
                        
                    };
                })
                .AddApplicationPart(typeof(AssemblyInformation).Assembly); // Register Required Services by ASP.NET Core Web APIs to DI Container


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddPersistanceServices(webApplicationBuilder.Configuration);

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
            app.UseMiddleware<CustomExceptionHandllerMiddleware>();

            app.MapControllers();

            app.UseStaticFiles();

            #endregion

            app.Run();
        }
    }
}
