using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Data.Entities.Entities;
using Data.Entities.Repositories;

namespace FileDataProvider.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : IIdentificatable, new()
    {
        public BaseRepository(string filePath)
        {
            this.filePath = filePath;
        }

        protected readonly string filePath;

        protected abstract T readItem(StreamReader sr);
        protected abstract void writeItem(T item, StreamWriter sw);

        protected virtual bool hasAccess(T item, int parentId) => true;

        private int getNextId()
        {
           
            int id = 1;
            using(var fs = new FileStream(this.filePath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        var item = readItem(sr);
                        if (item.Id > id)
                            id = item.Id + 1;
                    }
                }
            }

            return id + 1;

        }

        public void Add(T item)
        {
            item.Id = getNextId();

            using (var fs = new FileStream(this.filePath, FileMode.Append))
            {
                using (var sw = new StreamWriter(fs))
                {
                    writeItem(item, sw);
                }
            }
            
        }

        public ICollection<T> GetAll(int parentId = -1)
        {
            ICollection<T> items = new List<T>();
            using(var fs = new FileStream(this.filePath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    while(!sr.EndOfStream)
                    {
                        var item = readItem(sr);
                        if (hasAccess(item, parentId))
                            items.Add(item);
                    }
                }
            }

            return items;
        }
        public T GetById(int id)
        {
            using (var fs = new FileStream(this.filePath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        T item = readItem(sr);
                        if (item.Id == id)
                            return item;

                    }
                }
            }
            return default(T);
        }

        public void Update(T item)
        {
            var fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            var fileFolder = filePath.Substring(0, filePath.LastIndexOf("\\"));

            string tempFilePath = $"{fileFolder}temp.{fileName}";

            using (var fsTemp = new FileStream(tempFilePath, FileMode.OpenOrCreate))
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        using (var sw = new StreamWriter(fsTemp))
                        {
                            while(!sr.EndOfStream)
                            {
                                T current = readItem(sr);
                                if (current.Id != item.Id)
                                    writeItem(current, sw);
                                else
                                    writeItem(item, sw);

                            }
                        }
                    }
                }
            }
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public void Delete(T item)
        {
            var fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            var fileFolder = filePath.Substring(0, filePath.LastIndexOf("\\"));

            string tempFilePath = $"{fileFolder}temp.{fileName}";

            using (var fsTemp = new FileStream(tempFilePath, FileMode.OpenOrCreate))
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        using (var sw = new StreamWriter(fsTemp))
                        {
                            while (!sr.EndOfStream)
                            {
                                T current = readItem(sr);
                                if (current.Id != item.Id)
                                    writeItem(current, sw);
                            }
                        }
                    }
                }
            }
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public void Save(T item)
        {
            if (item.Id > 0)
                Update(item);
            else
                Add(item);
        }

        public void Save()
        {
            
        }

        public int Count()
        {
            return this.GetAll().Count;
        }

        public void Dispose()
        {
            
        }

        public ICollection<T> GetAll<TKey>(int itemsPerPage = 0, int page = 0, Expression<Func<T, bool>> @where = null, Expression<Func<T, TKey>> orderByKeySelector = null,
            bool @descending = false)
        {
            var items = this.GetAll().Where(where?.Compile());
            items = descending
                ? items.OrderByDescending(orderByKeySelector?.Compile())
                : items.OrderBy(orderByKeySelector?.Compile());
            if (itemsPerPage > 0 && page > 0)
                return items
                    .Skip(itemsPerPage * (page - 1))
                    .Take(itemsPerPage)
                    .ToList();
            else return items.ToList();
        }

        public ICollection<TResult> GetAll<TKey, TResult>(int itemsPerPage = 0, int page = 0, Expression<Func<T, bool>> @where = null,
            Expression<Func<T, TKey>> orderByKeySelector = null, bool @descending = false, Expression<Func<T, TResult>> @select = null)
        {
            var items = this.GetAll().Where(where?.Compile());
            items = descending
                ? items.OrderByDescending(orderByKeySelector?.Compile())
                : items.OrderBy(orderByKeySelector?.Compile());
            var result = items.Select(select?.Compile());
            if (itemsPerPage > 0 && page > 0)
                return result
                    .Skip(itemsPerPage * (page - 1))
                    .Take(itemsPerPage)
                    .ToList();
            else return result.ToList();
        }

        public TResult FirstOrDefault<TResult>(Expression<Func<T, bool>> @where, Expression<Func<T, TResult>> @select = null)
        {
            return this.GetAll().Where(where.Compile()).Select(select?.Compile()).FirstOrDefault();
        }

        public ICollection<T> GetAll(int itemsPerPage = 0, int page = 0, Expression<Func<T, bool>> @where = null)
        {
            var items = this.GetAll().Where(where?.Compile());
            if (itemsPerPage > 0 && page > 0)
                return items
                    .Skip(itemsPerPage * (page - 1))
                    .Take(itemsPerPage)
                    .ToList();
            else return items.ToList();
        }

        public ICollection<T> GetAll()
        {
            return this.GetAll(-1);
        }

        public T FirstOrDefault(Func<T, bool> @where)
        {
            return this.GetAll().Where(where).FirstOrDefault();
        }
    }
}
