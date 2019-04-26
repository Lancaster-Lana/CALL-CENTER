
using Laneta.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Laneta.EntityFramework
{
    public interface IRepository<T> : IRepository<T, int> where T : Entity<int>
    {
    }

    public interface IRepository<T, TKey> where T : Entity<TKey> 
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T Find(TKey id);

        T FindIncluding(TKey key, params Expression<Func<T, object>>[] includeProperties);

        void InsertOrUpdate(T alert);

        void Delete(TKey id);

        void Save();
    }
}
