
using FileDataProvider.Entities;
using System;
using System.Collections.Generic;
using System.IO;

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

            return id;

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

        public ICollection<T> GetAll()
        {
            ICollection<T> items = new List<T>();
            using(var fs = new FileStream(this.filePath, FileMode.OpenOrCreate))
            {
                using (var sr = new StreamReader(fs))
                {
                    while(sr.EndOfStream)
                    {
                        items.Add(readItem(sr));
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
                    while (sr.EndOfStream)
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
    }
}
