using System;
using System.Linq;
using System.Linq.Expressions;
using Laneta.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laneta.EntityFramework
{
    public class Repository<T> : Repository<T, int> where T : Entity<int>
    {
        public Repository(DbContext context) : base(context)
        {
        }
    }

    public class Repository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey> 
    {
        //private readonly AppDBContext context = new AppDBContext();

        protected readonly DbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> All
        {
            get { return this._entities; }
        }

        public T FindIncluding(TKey key, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this._entities;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(q=>q.ID.Equals(key));
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this._entities;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public T Find(TKey id)
        {
            return this._entities.Find(id);
        }

        public void InsertOrUpdate(T entity)
        {
            if (entity.ID.Equals(default(TKey)))
            {
                this._entities.Add(entity);
            }
            else
            {
                //this._entities.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public void Delete(TKey id)
        {
            var entity = this.Find(id);
            this._entities.Remove(entity);
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }
}
