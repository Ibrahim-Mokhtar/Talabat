using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;

namespace Talabat.Core.Domain.Entites.Products
{
    public class ProductCategory:BaseEntity<int>
    {
        public required string Name { get; set; }
    }
}
