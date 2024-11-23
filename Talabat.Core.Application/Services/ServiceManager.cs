using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Employees;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Services.Basket;
using Talabat.Core.Application.Services.Employees;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contracts.Infrastructure;
using Talabat.Core.Domain.Contracts.Persistence;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IBasketServices> _basketServices;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,IConfiguration configuration,Func<IBasketServices> basketServiceFactory, Func<IAuthService> authServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _employeeService=new Lazy<IEmployeeService>(()=>new EmployeeService(_unitOfWork, _mapper));
            _basketServices = new Lazy<IBasketServices>(basketServiceFactory);

            _authService = new Lazy<IAuthService>(authServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IProductService ProductService => _productService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;

        public IBasketServices BasketServices => _basketServices.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
