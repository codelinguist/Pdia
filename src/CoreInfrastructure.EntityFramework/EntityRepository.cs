﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreInfrastructure.EntityFramework
{
    public sealed class EntityRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        DbContext _context;
        public EntityRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Items
        {
            get { return _context.Set<T>(); }
        }

        public Task<List<T>> ItemsWithAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = null;
            if (predicate != null)
                query = _context.Set<T>().Where(predicate);
            else
                query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            

            return query.ToListAsync();
        }

        public Task<List<T>> ItemsAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return _context.Set<T>().Where(predicate).ToListAsync();
            else
                return _context.Set<T>().ToListAsync();
        }
        public IEnumerable<T> ItemsWith(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T Find(params object[] keyValues)
        {
            return _context.Set<T>().Find(keyValues);
        }

        public void Attach(T item)
        {
            _context.Set<T>().Attach(item);
        }

        public void InsertGraph(T graph)
        {

        }

        public void Insert(T item)
        {
            _context.Set<T>().Add(item);
        }


        public void Update(T item, params Expression<Func<T, object>>[] excludeProperties)
        {

            _context.Set<T>().Attach(item);

            var entry = _context.Entry(item);
            entry.State = System.Data.Entity.EntityState.Modified;
            foreach (var excludeProperty in excludeProperties)
            {
                entry.Property(excludeProperty).IsModified = false;
            }
        }
        //Automaticaly saves changes.
        public void UpdateProperty(T item, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Set<T>().Attach(item);
                //var entry = _context.Entry(item);
                //entry.State = EntityState.Modified;
                _context.Set<T>().Attach(item);
                var entry = _context.Entry(item);
                foreach (var includeProperty in includeProperties)
                {
                    entry.Property(includeProperty).IsModified = true;
                }
                _context.SaveChanges();
            }
            finally
            {
                _context.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public void Delete(T item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public void Delete(object id)
        {
            var item = _context.Set<T>().Find(id);
            if (item != null)
                _context.Set<T>().Remove(item);
        }



        public Task<T> FindAsync(params object[] keyValues)
        {
            return _context.Set<T>().FindAsync(keyValues);
        }


    }
}
