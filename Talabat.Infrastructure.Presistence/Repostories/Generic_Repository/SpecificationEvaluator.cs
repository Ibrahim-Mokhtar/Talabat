using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;

namespace Talabat.Infrastructure.Presistence.Repostories.Generic_Repository
{
    internal static class SpecificationEvaluator<TEntity,TKey>
        where TEntity:BaseAuditableEntity<TKey>
        where TKey:IEquatable<TKey>
    {
        public static IQueryable<TEntity>GetQuery(IQueryable<TEntity> inputQuery,ISpecfcations<TEntity,TKey> spec)
        {
            var query = inputQuery;
            if (spec.Criteria is not null) // E => E.Id == 10
                query = query.Where(spec.Criteria);
            // query = _dbContext.Set<Product>().Where(E => E.Id == 10);
            // includes expressions
            // 1. P => P.Brand
            // 2. P => P.Category
            // ...

            query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            // query = _dbContext.Set<Product>().Include(P => P.Brand);
            // query = _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category);

            return query;

        }
    }
}
