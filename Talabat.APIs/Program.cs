
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
using Talabat.Infrastructure;
using Talabat.Core.Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Talabat.Infrastructure.Presistence._Identity;

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
                                                            .Select(P => new ApiValidationErrorResponse.ValidationError
                                                            {
                                                                Field = P.Key,
                                                                Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
                                                            });
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

            webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                //identityOptions.Password.RequireNonAlphanumeric = true; // $#@%
                //identityOptions.Password.RequiredUniqueChars = 2;
                //identityOptions.Password.RequiredLength = 6;
                //identityOptions.Password.RequireDigit = true;
                //identityOptions.Password.RequireLowercase = true;
                //identityOptions.Password.RequireUppercase = true;

                identityOptions.User.RequireUniqueEmail = true;
                //identityOptions.User.AllowedUserNameCharacters = "abcdenfakdjsadsa";

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);



            })
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            #endregion

            var app = webApplicationBuilder.Build();

            #region Database Initialization
            await app.InitializeDbAsync();
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
            app.UseMiddleware<ExceptionHandllerMiddleware>();
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.MapControllers();

            app.UseStaticFiles();

            #endregion

            app.Run();
        }
    }
}
