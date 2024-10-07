using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Products.Models
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int? BrandId { get; set; }
        public  string? Brand { get; set; } // Foriegn Key --> ProductBrand Entity
        public int? CategoryId { get; set; }
        public  string? Category { get; set; } // Foriegn Key --> ProductCategory Entity
    }
}
