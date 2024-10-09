using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandandCategorySpecification:BaseSpecifications<Product,int>
    {
        public ProductWithBrandandCategorySpecification()
            :base()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);

        }
    }
}
