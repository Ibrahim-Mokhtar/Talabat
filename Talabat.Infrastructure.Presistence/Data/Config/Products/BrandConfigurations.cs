using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Infrastructure.Presistence.Data.Config.Base;

namespace Talabat.Infrastructure.Presistence.Data.Config.Products
{
    internal class BrandConfigurations:BaseAuditableEntityConfigurations<ProductBrand,int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                .IsRequired();
        }
    }
}
