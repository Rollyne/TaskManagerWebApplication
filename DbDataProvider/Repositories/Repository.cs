
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TEntity GetById(int id)
        {
            return Context.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public virtual ICollection<TEntity> GetAll(int parentId = -1)
        {
            if (parentId != -1)
                return this.Where(i => IsParent(parentId, i));
            return Context.Set<TEntity>().ToList();
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

        public TEntity FirstOrDefault(Func<TEntity, bool> condition)
        {
            return Context.Set<TEntity>().FirstOrDefault(condition);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public virtual ICollection<TEntity> GetAmountBySkipping(int skip, int amount, int parentId = -1)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (parentId != -1)
                query = query.Where(i => IsParent(parentId, i));
            return query.Skip(skip).Take(amount).ToList();
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }
    }
}
