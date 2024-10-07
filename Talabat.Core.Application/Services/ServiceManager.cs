using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Application.Abstraction.Products;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contracts;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly Lazy<ProductService> _productService;

        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _productService = new Lazy<ProductService>(()=>new ProductService(unitOfWork, mapper));
        }
        public IProductService ProductService =>_productService.Value;
    }
}
