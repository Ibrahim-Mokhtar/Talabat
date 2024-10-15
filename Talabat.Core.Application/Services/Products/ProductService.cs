using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Common;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Domain.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Core.Domain.Specifications.Products;

namespace Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandandCategorySpecification(specParams.Sort,specParams.BrandId,specParams.CategoryId,specParams.PageSize,specParams.PageIndex,specParams.Search);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(specs);

            var data = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFiltirationsToCountSpecifications(specParams.BrandId, specParams.CategoryId,specParams.Search);
            
            var count=await unitOfWork.GetRepository<Product,int>().GetCountAsync(countSpec);

            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize,count) { Data=data};
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {

            var specs = new ProductWithBrandandCategorySpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(specs);
            var productToReturn = mapper.Map<ProductToReturnDto>(product);

            return productToReturn;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandDto = mapper.Map<IEnumerable<BrandDto>>(brands);

            return brandDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var categoryDto = mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoryDto;
        }
    }
}
