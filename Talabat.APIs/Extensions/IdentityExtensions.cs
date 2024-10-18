using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Services.Auth;
using Talabat.Core.Domain.Entites.Identity;
using Talabat.Infrastructure.Presistence._Identity;

namespace Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                //identityOptions.SignIn.RequireConfirmedAccount = true;
                //identityOptions.SignIn.RequireConfirmedEmail = true;
                //identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

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

            services.AddAuthentication(authinticationOptions =>
            {
                authinticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authinticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = configuration["JWTSettings:Audience"],
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
                return ()=>serviceProvider.GetRequiredService<IAuthService>();
            });
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            return services;
        }
    }
}
