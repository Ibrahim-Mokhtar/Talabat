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
    internal class CategoryConfigurations : BaseAuditableEntityConfigurations<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(C => C.Name)
                .IsRequired();
        }

    }
}
