using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Core.Domain.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();
    }
}
