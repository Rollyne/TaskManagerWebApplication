

using Data.Entities.Entities;

namespace Data.Entities.Repositories
{
    public interface IRepositoryProvider
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IIdentificatable, new();
        IRepository<User> GetUserRepository();
        IRepository<Task> GetTaskRepository();
        IRepository<Comment> GetCommentRepository();
    }
}
