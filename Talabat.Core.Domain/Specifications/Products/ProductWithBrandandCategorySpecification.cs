using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandandCategorySpecification : BaseSpecifications<Product, int>
    {
        // This Object is Created via this Constructor, Will be Used for Building the Query that Get ALL Products
        public ProductWithBrandandCategorySpecification(string? sort, int? brandId, int? categoryId)
            : base(
                  P=>
                  ((!brandId.HasValue)||P.BrandId==brandId)
                  &&
                  ((!categoryId.HasValue) || P.CategoryId == categoryId)
                  )
        {
            AddIncludes();


            switch (sort)
            {
                case "nameDesc":
                    AddOrderByDesc(P => P.Name);
                    break;
                case "priceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Name);
                    break;
            }

        }
        // This Object is Created via this Constructor, Will be Used for Building the Query that Get Specific Product
        public ProductWithBrandandCategorySpecification(int id)
       : base(id)
        {
            AddIncludes();

        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

    }
}
