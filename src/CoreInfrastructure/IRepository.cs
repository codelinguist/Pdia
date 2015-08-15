using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreInfrastructure
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Items { get; }
        Task<List<TEntity>> ItemsAsync(Expression<Func<TEntity, bool>> predicate = null);
        IEnumerable<TEntity> ItemsWith(params Expression<Func<TEntity, object>>[] includeProperties);
        void Attach(TEntity item);
        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        void Insert(TEntity item);
        void Update(TEntity item, params Expression<Func<TEntity, object>>[] excludeProperties);
        void UpdateProperty(TEntity item, params Expression<Func<TEntity, object>>[] includeProperties);
        void Delete(TEntity item);
        void Delete(object id);
    }
}
