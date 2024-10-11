using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts;

namespace Talabat.Core.Domain.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity,bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get; set; }

        protected BaseSpecifications()
        {
            
        }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression; // true && true
        }
        protected BaseSpecifications (TKey id)
        {
            Criteria = E => E.Id.Equals(id);
        }
        private protected void AddOrderBy(Expression<Func<TEntity,object>> orderByExpression)
        {
            OrderBy = orderByExpression; // P => P.Name
        }
        private protected void AddOrderByDesc(Expression<Func<TEntity,object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }
        private protected virtual void AddIncludes()
        {
        }

        private protected void ApplyPagination(int skip,int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
