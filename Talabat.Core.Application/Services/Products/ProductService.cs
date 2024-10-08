using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Products;
using Talabat.Core.Application.Abstraction.Products.Models;
using Talabat.Core.Domain.Contracts.Presistance;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Core.Domain.Specifications;
using Talabat.Core.Domain.Specifications.Product_Specs;

namespace Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        {
            var specs = new ProductWithBrandAndCategorySpecifications();
            var products = mapper.Map<IEnumerable<ProductToReturnDto>>(await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(specs));
            return products;
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var specs=new ProductWithBrandAndCategorySpecifications(id);
            var product= mapper.Map<ProductToReturnDto>(await unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(specs));
            return product;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());

    }
}
