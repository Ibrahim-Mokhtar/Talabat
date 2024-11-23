using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Orders;
using Talabat.Infrastructure.Presistence.Data.Config.Base;

namespace Talabat.Infrastructure.Presistence._Data.Config.Orders
{
    internal class DeliveryMethodConfigurations:BaseEntityConfigurations<DeliveryMethod,int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);

            builder.Property(method => method.Cost)
                .HasColumnType("decimal(8,2)");
        }
    }
}
