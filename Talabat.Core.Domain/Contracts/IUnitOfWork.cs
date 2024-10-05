using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<Product, int> ProductRepository { get; }
        public IGenericRepository<ProductBrand, int> BrandsRepository { get;  }
        public IGenericRepository<ProductCategory, int> CategoriesRepository { get;  }

        Task<int> CompleteAsync();
    }
}
