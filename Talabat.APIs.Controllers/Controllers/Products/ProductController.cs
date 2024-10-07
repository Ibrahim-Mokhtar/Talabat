using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Controllers.Base;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Application.Abstraction.Products.Models;

namespace Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductController(IServiceManager serviceManager) : ApiControllerBase
    {

        [HttpGet] // GET: /api/Product
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var products = await serviceManager.ProductService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")] // GET: /api/Product
        public async Task<ActionResult<ProductToReturnDto>> GetProducts(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            if(product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("brands")] // GET: /api/Product/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands =await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")] // GET: /api/Product/categories
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories =await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
