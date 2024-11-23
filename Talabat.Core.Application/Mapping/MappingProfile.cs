using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Models.Common;
using Talabat.Core.Application.Abstraction.Models.Employees;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Domain.Entites.Basket;
using Talabat.Core.Domain.Entites.Employees;
using Talabat.Core.Domain.Entites.Orders;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Application.Mapping
{
    internal class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(s => s.Brand, O => O.MapFrom(src => src.Brand!.Name))
                .ForMember(s => s.Category, O => O.MapFrom(src => src.Category!.Name))
                .ForMember(s=>s.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductCategory, CategoryDto>();
            
            CreateMap<Employee, EmployeeToReturnDto>()
                .ForMember(s => s.Department, O => O.MapFrom(src => src.Department!.Name));

            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<Address, AddressDto>();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
