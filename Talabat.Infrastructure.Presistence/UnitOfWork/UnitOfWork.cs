using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Infrastructure.Presistence.Repostorie;

namespace Talabat.Infrastructure.Presistence.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            //return new GenericRepository<TEntity, TKey>(_dbContext);

            /// var typeName=typeof(TEntity).Name; // Product
            /// if(_repositories.ContainsKey(typeName)) return (IGenericRepository<TEntity,TKey>)_repositories[typeName];
            ///
            /// var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            /// _repositories.Add(typeName, repository);
            ///
            /// return repository;

            return (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    }
}
