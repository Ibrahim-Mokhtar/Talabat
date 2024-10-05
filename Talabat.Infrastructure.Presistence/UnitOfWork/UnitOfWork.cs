using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Infrastructure.Presistence.Repostorie;

namespace Talabat.Infrastructure.Presistence.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly Lazy<IGenericRepository<Product, int>> _productRepository;
        private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandrRepository;
        private readonly Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
            _brandrRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
            _categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));
        }

        public IGenericRepository<Product, int> ProductRepository => _productRepository.Value;
        public IGenericRepository<ProductBrand, int> BrandsRepository => _brandrRepository.Value;
        public IGenericRepository<ProductCategory, int> CategoriesRepository => _categoryRepository.Value;

        public Task<int> CompleteAsync() => _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
    }
}
