using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Application.Services
{
    internal class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        {
            var products =await unitOfWork.GetRepository<Product, int>().GetAllAsync();
            
            var productsToReturn = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            return productsToReturn;
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var productToReturn=mapper.Map<ProductToReturnDto>(product);

            return productToReturn;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands=await unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            var brandDto=mapper.Map<IEnumerable<BrandDto>>(brands);

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
