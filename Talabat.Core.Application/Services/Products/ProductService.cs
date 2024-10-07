using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Products;
using Talabat.Core.Application.Abstraction.Products.Models;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
            => mapper.Map<IEnumerable<ProductToReturnDto>>(await unitOfWork.GetRepository<Product, int>().GetAllAsync());

        public async Task<ProductToReturnDto> GetProductAsync(int id)
            => mapper.Map<ProductToReturnDto>(await unitOfWork.GetRepository<Product, int>().GetAsync(id));

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => mapper.Map<IEnumerable<CategoryDto>>(await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());

    }
}
