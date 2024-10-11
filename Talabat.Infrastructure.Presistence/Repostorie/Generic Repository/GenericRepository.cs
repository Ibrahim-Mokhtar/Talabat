using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Infrastructure.Presistence.Data;
using Talabat.Infrastructure.Presistence.Repostorie.Generic_Repository;

namespace Talabat.Infrastructure.Presistence.Repostorie.Generic_Repsitory
{
    public class GenericRepository<TEntity, Tkey>(StoreContext DbContext) : IGenericRepository<TEntity, Tkey>
        where TEntity : BaseAuditableEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
                return WithTracking ?
                    (IEnumerable<TEntity>)await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() :
                     (IEnumerable<TEntity>)await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsTracking().ToListAsync();
            return WithTracking ?
            await DbContext.Set<TEntity>().ToListAsync() :
            await DbContext.Set<TEntity>().AsTracking().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, Tkey> spec, bool WithTracking = false)
        => WithTracking ?
                await ApplySpecifications(spec).ToListAsync() :
                await ApplySpecifications(spec).AsNoTracking().ToListAsync();


        public async Task<TEntity?> GetAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
                return await DbContext.Set<Product>().Where(P => P.Id.Equals(id)).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as TEntity;
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, Tkey> spec)
        => await ApplySpecifications(spec).FirstOrDefaultAsync();

        public async Task<int> GetCountAsync(ISpecifications<TEntity, Tkey> spec)
        => await ApplySpecifications(spec).CountAsync();
        public async Task AddAsync(TEntity entity) => await DbContext.Set<TEntity>().AddAsync(entity);

        public async void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => DbContext.Set<TEntity>().Remove(entity);


        #region Helpers

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, Tkey> spec)
        {
            return SpecificationsEvaluater<TEntity, Tkey>.GetQuery(DbContext.Set<TEntity>(), spec);
        }

        #endregion
    }

}
