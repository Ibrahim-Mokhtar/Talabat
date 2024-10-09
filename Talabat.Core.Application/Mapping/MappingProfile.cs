using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Application.Mapping
{
    internal class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(s => s.Brand, O => O.MapFrom(src => src.Brand!.Name))
                .ForMember(s => s.Category, O => O.MapFrom(src => src.Category!.Name));

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductCategory, CategoryDto>();
        }
    }
}
