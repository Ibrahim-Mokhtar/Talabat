using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entites.Products;

namespace Talabat.Infrastructure.Presistence.Repostorie.Generic_Repository
{
    internal static class SpecificationsEvaluater<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey:IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecifications<TEntity,TKey> spec)
        {
            var query = inputQuery; // _dbContext.Set<Product>

            if(spec.Criteria is not null) // P => P.BrandId == 1 && P.CategoryId == 1
                query =query.Where(spec.Criteria); 

            // query = _dbContext.Set<Product>.Where( P => P.BrandId == 1 && P.CategoryId == 1)
            // include expressions
            // 1. P => P.Brand
            // 2. P => P.Category
            // ...

            if (spec.OrderBy is not null) // P => P.Price
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDesc is not null) 
                query = query.OrderByDescending(spec.OrderByDesc);

            // query = _dbContext.Set<Product>().OrderByDescending(P => P.Price)


            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            // query = _dbContext.Set<Product>().Where(P => P.BrandId == 1 && P.CategoryId == 1).OrderBy(P => P.Price).Include(P => P.Brand);
            // query = _dbContext.Set<Product>().Where(P => P.BrandId == 1 && P.CategoryId == 1).OrderBy(P => P.Price).Include(P => P.Brand).Include(P => P.Category);

            return query;
        }

    }
}
