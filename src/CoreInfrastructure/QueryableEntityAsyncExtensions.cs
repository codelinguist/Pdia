using CoreInfrastructure.DataModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreInfrastructure
{
    public static class QueryableEntityAsyncExtensions
    {
        private static Lazy<IKernel> Kernel = new Lazy<IKernel>(() => AppInfrastructure.GlobalKernel, true);
        public static Task<List<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> source) where TEntity : IEntity
        {
            //Resolve 
            return Kernel.Value.Get<IAsyncQueryProxy>().ToListAsync(source);

        }
        public static Task<List<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> source, CancellationToken cancellationToken) where TEntity : IEntity
        {
            return Kernel.Value.Get<IAsyncQueryProxy>().ToListAsync(source, cancellationToken);
        }

        public static Task<List<TEntity>> CountAsync<TEntity>(this IQueryable<TEntity> source, CancellationToken cancellationToken) where TEntity : IEntity
        {
            return Kernel.Value.Get<IAsyncQueryProxy>().ToListAsync(source, cancellationToken);
        }

        public static Task<int> CountAsync<TEntity>(this IQueryable<TEntity> source) where TEntity : IEntity
        {
            return Kernel.Value.Get<IAsyncQueryProxy>().CountAsync(source);
        }

        public static Task<int> CountAsync<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate) where TEntity : IEntity
        {
            return Kernel.Value.Get<IAsyncQueryProxy>().CountAsync(source, predicate);
        }


        public static Task<int> CountAsync<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : IEntity
        {
            return Kernel.Value.Get<IAsyncQueryProxy>().CountAsync(source, predicate, cancellationToken);
        }


        public static Task<IPagedList<TEntity>> ToPagedListAsync<TEntity>(IQueryable<TEntity> source, int pageNumber, int pageSize) where TEntity : IEntity
        {
            return PagedList<TEntity>.CreateAsync(source, pageNumber, pageSize);
        }

        public static Task<IPagedList<T>> ToPagedListAsync<T>(IQueryable<IEntity> source, Func<IEntity, T> select, int pageNumber, int pageSize)
        {
            //return PagedList<T>.CreateAsync(source, pageNumber, pageSize);
            throw new NotImplementedException();
        }
        public static Task ForEachAsync<TEntity>(this IQueryable<TEntity> source, Action<TEntity> action) where TEntity:IEntity
        {
            return Kernel.Value.Get<IAsyncQueryProxy>().ForEachAsync(source, action);
        }

        public static Task ForEachAsync<TEntity>(this IQueryable<TEntity> source, Action<TEntity> action, CancellationToken cancellationToken) where TEntity : IEntity
        {
                
            return Kernel.Value.Get<IAsyncQueryProxy>().ForEachAsync(source, action, cancellationToken);
        }

        //TODO: INCLUDE methods
    }
}
