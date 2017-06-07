using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Entities.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        void Add(T item);
        ICollection<T> GetAll();
        ICollection<T> GetAll(
            int itemsPerPage = 0,
            int page = 0,
            Expression<Func<T, bool>> where = null);
        ICollection<T> GetAll<TKey>(
            int itemsPerPage = 0,
            int page = 0,
            Expression<Func<T, bool>> where = null,
            Expression<Func<T, TKey>> orderByKeySelector = null,
            bool descending = false);
        ICollection<TResult> GetAll<TKey, TResult>(
            int itemsPerPage = 0,
            int page = 0,
            Expression<Func<T, bool>> where = null,
            Expression<Func<T, TKey>> orderByKeySelector = null,
            bool descending = false,
            Expression<Func<T, TResult>> select = null);

        int Count();

        void Update(T item);

        void Delete(T item);

        T FirstOrDefault(Func<T, bool> where);

        TResult FirstOrDefault<TResult>(
            Expression<Func<T, bool>> where,
            Expression<Func<T, TResult>> select = null);

        void Save();
    }
}