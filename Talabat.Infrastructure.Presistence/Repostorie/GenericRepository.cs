using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entites.Products;
using Talabat.Infrastructure.Presistence.Data;

namespace Talabat.Infrastructure.Presistence.Repostorie
{
    public class GenericRepository<TEntity, Tkey>(StoreContext DbContext) : IGenericRepository<TEntity, Tkey>
        where TEntity : BaseAuditableEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {
            //if (typeof(TEntity) == typeof(Product))
            //{
            //    if (WithTracking)
            //       return await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
            //   return await DbContext.Set<Product>().AsNoTracking().ToListAsync();

            //}
            if (WithTracking)
               return await DbContext.Set<TEntity>().ToListAsync();
           return await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(Tkey id)
        {
            //if (typeof(TEntity) == typeof(Product))
            //    return await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).FirstOrDefault(P => P.Id == id);

            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity) => await DbContext.Set<TEntity>().AddAsync(entity);

        public async void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => DbContext.Set<TEntity>().Remove(entity);

    }

}
