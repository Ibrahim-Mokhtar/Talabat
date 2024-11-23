using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.API.Controllers.Errors;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Common;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Domain.Application.Abstraction.Models.Products;

namespace Talabat.API.Controllers.Controllers.Products
{
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class ProductController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet] // GET : /api/Product
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetProductsAsync(specParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")] // GET : /api/Products/id
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            return Ok(product);
        }

        [HttpGet("brands")] // GET : /api/Products/brands
        public async Task<ActionResult<BrandDto>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")] // GET : /api/Products/categories
        public async Task<ActionResult<CategoryDto>> GetCategories()
        {
            var categories = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
