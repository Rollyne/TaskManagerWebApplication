
using Data.Entities.Entities;
using Data.Entities.Repositories;

namespace DbDataProvider.Repositories
{
    public class DbRepositoryProvider : IRepositoryProvider
    {
        private TaskManagerContext context;
        public DbRepositoryProvider(TaskManagerContext context)
        {
            this.context = context;
        }
        public IRepository<User> GetUserRepository()
        {
            return new Repository<User>(this.context);
        }

        public IRepository<Task> GetTaskRepository()
        {
            return new Repository<Task>(this.context);
        }

        public IRepository<Comment> GetCommentRepository()
        {
            return new Repository<Comment>(this.context);
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IIdentificatable, new()
        {
            return new Repository<TEntity>(this.context);
        }
    }
}
