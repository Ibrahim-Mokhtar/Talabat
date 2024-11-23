using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Identity;
using Talabat.Infrastructure.Presistence._Common;

namespace Talabat.Infrastructure.Presistence.Identity.Config
{
    [DbContextType(typeof(StoreIdentityDbContext))]
    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.DispalyName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");
            builder.HasOne(U => U.Address)
                .WithOne(A => A.AppUser)
                .HasForeignKey<Address>(A => A.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
