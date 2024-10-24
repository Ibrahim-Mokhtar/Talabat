using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Domain.Entites.Identity;
using Talabat.Infrastructure.Presistence.Identity;

namespace Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services) 
        { 
            
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

            return services;    

               
        }
    }
}
