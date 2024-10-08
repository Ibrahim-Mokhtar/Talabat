using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Contracts.Presistance
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecfcations<TEntity, TKey> spec,bool WithTracking = false);
        Task<TEntity> GetAsync(TKey? id);
        Task<TEntity> GetWithSpecAsync(ISpecfcations<TEntity,TKey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
