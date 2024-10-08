using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Domain.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        // This Object is Created via this Constructor, Will be Used for Building the Query that Get ALL Products
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

        // This Object is Created via this Constructor, Will be Used for Building the Query that Get Specific Product
        public ProductWithBrandAndCategorySpecifications(int id)
            : base(id)
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
