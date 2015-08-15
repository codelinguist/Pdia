using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using CoreInfrastructure.DataModel;

namespace CoreInfrastructure.EntityFramework
{
    public class EntityFrameworkQueryAsyncProxy : IAsyncQueryProxy
    {
        public Task<int> CountAsync<TEntity>(IQueryable<TEntity> source) where TEntity : IEntity
        {
            return QueryableExtensions.CountAsync(source);
        }

        public Task<int> CountAsync<TEntity>(IQueryable<TEntity> source, CancellationToken cancellationToken) where TEntity : IEntity
        {
            return QueryableExtensions.CountAsync(source, cancellationToken);
        }

        public Task<int> CountAsync<TEntity>(IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate) where TEntity : IEntity
        {
            return QueryableExtensions.CountAsync(source, predicate);
        }

        public Task<int> CountAsync<TEntity>(IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)where TEntity : IEntity
        {
            return QueryableExtensions.CountAsync(source, predicate, cancellationToken);
        }

        public Task ForEachAsync<TEntity>(IQueryable<TEntity> source, Action<TEntity> action) where TEntity : IEntity
        {
            return QueryableExtensions.ForEachAsync(source, action);
        }

        public Task ForEachAsync<TEntity>(IQueryable<TEntity> source, Action<TEntity> action, CancellationToken cancellationToken) where TEntity : IEntity
        {
            return QueryableExtensions.ForEachAsync(source, action, cancellationToken);
        }

        public Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> source) where TEntity : IEntity
        {

            return QueryableExtensions.ToListAsync(source);
        }

        public Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> source, CancellationToken cancellationToken) where TEntity : IEntity
        {
            return QueryableExtensions.ToListAsync(source, cancellationToken);
        }

    }
}
