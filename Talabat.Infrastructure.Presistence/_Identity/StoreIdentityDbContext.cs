using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Identity;
using Talabat.Infrastructure.Presistence._Identity.Config;

namespace Talabat.Infrastructure.Presistence._Identity
{
    internal class StoreIdentityDbContext:IdentityDbContext<ApplicationUser> 
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
            :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new AddressConfigurations());
        }
    }
}
