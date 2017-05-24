

using Data.Entities.Entities;

namespace Data.Entities.Repositories
{
    public interface IRepositoryProvider
    {
        IRepository<User> GetUserRepository();
        IRepository<Task> GetTaskRepository();
        IRepository<Comment> GetCommentRepository();
    }
}
