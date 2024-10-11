using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithFiltirationsToCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithFiltirationsToCountSpecifications(int? brandId, int? categoryId)
            : base(
                  P =>
                  (!brandId.HasValue || P.BrandId == brandId.Value)
                  &&
                  (!categoryId.HasValue || P.CategoryId == categoryId.Value)
                  )
        {

        }
    }
}
