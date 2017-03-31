﻿using System.Collections.Generic;

namespace FileDataProvider.Repositories
{
    public interface IRepository<T>
    {
        void Add(T item);

        T GetById(int id);
        ICollection<T> GetAll(int parentId = -1);

        void Update(T item);

        void Delete(T item);
    }
}