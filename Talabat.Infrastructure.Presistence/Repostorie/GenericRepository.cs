using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;
using Talabat.Infrastructure.Presistence.Data;

namespace Talabat.Infrastructure.Presistence.Repostorie
{
    public class GenericRepository<TEntity, Tkey>(StoreContext DbContext) : IGenericRepository<TEntity, Tkey>
        where TEntity : BaseAuditableEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
       => WithTracking ? await DbContext.Set<TEntity>().ToListAsync() : await DbContext.Set<TEntity>().AsTracking().ToListAsync();
        public async Task<TEntity?> GetAsync(Tkey id) => await DbContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity) => await DbContext.Set<TEntity>().AddAsync(entity);

        public async void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => DbContext.Set<TEntity>().Remove(entity);

    }

}
