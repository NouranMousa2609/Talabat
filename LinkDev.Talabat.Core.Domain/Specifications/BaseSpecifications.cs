using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey>
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        protected BaseSpecifications()
        {

        }
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        protected BaseSpecifications(TKey id)
        {
            Criteria = E => E.Id.Equals(id);

        }

        private protected virtual void AddIncludes()
        {


        }

        private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }

        private protected void ApplyPagination(int skip, int take)
        {

            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
