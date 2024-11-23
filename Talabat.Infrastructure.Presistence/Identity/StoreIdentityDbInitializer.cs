using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using Talabat.Core.Domain.Entites.Identity;
using Talabat.Infrastructure.Presistence._Common;

namespace Talabat.Infrastructure.Presistence.Identity
{
    internal sealed class StoreIdentityDbInitializer(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager) : DbInitializer(dbContext), IStoreIdentityDbInitializer
    {

        public override async Task SeedAsync()
        {
            if (!dbContext.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DispalyName = "Ahmed Nasr",
                    UserName = "ahmed.nasr",
                    Email = "ahmed.nasr@gmail.com",
                    PhoneNumber = "01122334455"


                };
                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
