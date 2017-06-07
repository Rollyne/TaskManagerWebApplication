using Data.Entities.Entities;
using Data.Entities.Repositories;
using Microsoft.Extensions.Configuration;

namespace FileDataProvider.Repositories
{
    public class FileRepositoryProvider : IRepositoryProvider
    {
        public string DataPath { get; set; }
        private readonly IConfiguration config;
        public FileRepositoryProvider(IConfiguration config)
        {
            this.config = config;
            DataPath = config["DataSettings:DataPath"];
        }

        public IRepository<Comment> GetCommentRepository(string fileName)
        {
            return new CommentRepository($"{DataPath}\\{fileName}");
        }
        public IRepository<Comment> GetCommentRepository()
            => GetCommentRepository("comments.dat");

        public IRepository<Task> GetTaskRepository(string fileName)
        {
            return new TaskRepository($"{DataPath}\\{fileName}");
        }
        public IRepository<Task> GetTaskRepository()
            => GetTaskRepository("tasks.dat");

        public IRepository<User> GetUserRepository(string fileName)
        {
            return new UserRepository($"{DataPath}\\{fileName}");
        }
        public IRepository<User> GetUserRepository()
            => GetUserRepository("users.dat");

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IIdentificatable, new()
        {
            throw new System.NotImplementedException();
        }
    }
}
