using Laneta.DAL.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Laneta.DAL.Repository
{
    public interface IRepository<T> : IRepository<T, int> where T : Entity<int>
    {
    }

    public interface IRepository<T, TKey> where T : Entity<TKey> 
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T Find(TKey id);

        void InsertOrUpdate(T alert);

        void Delete(TKey id);

        void Save();
    }
}
