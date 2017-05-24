using System;
using System.Collections.Generic;

namespace Data.Entities.Repositories
{
    public interface IRepository<T>
    {
        void Add(T item);

        T GetById(int id);
        ICollection<T> GetAll(int parentId = -1);
        ICollection<T> GetAmountBySkipping(int skip, int amount, int parentId = -1);

        int Count();

        void Update(T item);

        void Delete(T item);

        ICollection<T> Where(Func<T, bool> condition);

        T FirstOrDefault(Func<T, bool> condition);

        void Save();
    }
}