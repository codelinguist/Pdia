using CoreInfrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreInfrastructure
{

    public interface IAsyncQueryProxy
    {
        Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> source) where TEntity : IEntity;

        Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> source, CancellationToken cancellationToken) where TEntity : IEntity;

        Task<int> CountAsync<TEntity>(IQueryable<TEntity> source) where TEntity : IEntity;

        Task<int> CountAsync<TEntity>(IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate) where TEntity : IEntity;

        Task<int> CountAsync<TEntity>(IQueryable<TEntity> source, CancellationToken cancellationToken) where TEntity : IEntity;

        Task<int> CountAsync<TEntity>(IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : IEntity;
        Task ForEachAsync<TEntity>(IQueryable<TEntity> source, Action<TEntity> action) where TEntity : IEntity;
        Task ForEachAsync<TEntity>(IQueryable<TEntity> source, Action<TEntity> action, CancellationToken cancellationToken) where TEntity : IEntity;
    }
}
