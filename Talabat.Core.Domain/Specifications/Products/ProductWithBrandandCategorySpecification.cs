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
        // This Object is Created via this Constructor, Will be Used for Building the Query that Get ALL Products
        public ProductWithBrandandCategorySpecification()
            :base()
        {
            AddIncludes();

        }

        // This Object is Created via this Constructor, Will be Used for Building the Query that Get Specific Product
        public ProductWithBrandandCategorySpecification(int id)
           : base(id)
        {
            AddIncludes();

        }
        private void AddIncludes()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
