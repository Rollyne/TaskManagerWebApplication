
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.Entities.Entities;
using Data.Entities.Repositories;

namespace DbDataProvider.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IIdentificatable
    {
        protected TaskManagerContext Context;
        public Repository(TaskManagerContext context)
        {
            Context = context;
        }

        protected virtual bool IsParent(int id, TEntity item) => true;

        public void Add(TEntity item)
        {
            Context.Set<TEntity>().Add(item);
        }

        public void Update(TEntity item)
        {
            Context.Set<TEntity>().Update(item);
        }

        public void Delete(TEntity item)
        {
            Context.Set<TEntity>().Remove(item);
        }

        public ICollection<TEntity> Where(Func<TEntity, bool> condition)
        {
            return Context.Set<TEntity>().Where(condition).ToList();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public void Dispose()
        {
            Context.Dispose();
        }


        public ICollection<TEntity> GetAll<TKey>(
            int itemsPerPage = 0,
            int page = 0,
            Expression<Func<TEntity, bool>> @where = null,
            Expression<Func<TEntity, TKey>> orderByKeySelector = null,
            bool descending = false)
        {
            IQueryable<TEntity> result = Context.Set<TEntity>();

            if (where != null)
            {
                result = result.Where(where);
            }
            if (orderByKeySelector != null)
            {
                result = descending ? result.OrderByDescending(orderByKeySelector) : result.OrderBy(orderByKeySelector);
            }
            if (itemsPerPage != 0 && page != 0)
            {
                var skip = (page - 1) * itemsPerPage;
                return result.Skip(skip).Take(itemsPerPage).ToList();
            }
                
            return result.ToList();
        }

        public ICollection<TResult> GetAll<TKey, TResult>(
            int itemsPerPage = 0,
            int page = 0,
            Expression<Func<TEntity, bool>> @where = null,
            Expression<Func<TEntity, TKey>> orderByKeySelector = null,
            bool descending = false,
            Expression<Func<TEntity, TResult>> @select = null)
        {
            IQueryable<TEntity> result = Context.Set<TEntity>();

            if (where != null)
            {
                result = result.Where(where);
            }
            if (orderByKeySelector != null)
            {
                result = descending ? result.OrderByDescending(orderByKeySelector) : result.OrderBy(orderByKeySelector);
            }

            if (itemsPerPage != 0 && page != 0)
            {
                var skip = (page - 1) * itemsPerPage;
                return result.Skip(skip).Take(itemsPerPage).Select(select).ToList();
            }
                
            return result.Select(select).ToList();
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> @where)
        {
            return Context.Set<TEntity>().FirstOrDefault(where);
        }

        public TResult FirstOrDefault<TResult>(Expression<Func<TEntity, bool>> @where, Expression<Func<TEntity, TResult>> @select = null)
        {
            return Context.Set<TEntity>().Where(where).Select(select).FirstOrDefault();
        }

        public ICollection<TEntity> GetAll(int itemsPerPage = 0, int page = 0, Expression<Func<TEntity, bool>> @where = null)
        {
            IQueryable<TEntity> result = Context.Set<TEntity>();

            if (where != null)
            {
                result = result.Where(where);
            }
            if (itemsPerPage != 0 && page != 0)
                return result.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            return result.ToList();
        }

        public ICollection<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
    }
}
