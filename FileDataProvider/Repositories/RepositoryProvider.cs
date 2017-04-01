using System;
using Microsoft.Extensions.Configuration;
using TaskManagerASP;
using TaskManagerASP.Configuration;

namespace FileDataProvider.Repositories
{
    public class RepositoryProvider : IRepositoryProvider
    {
        public string DataPath { get; set; }
        private readonly IConfiguration config;
        public RepositoryProvider(IConfiguration config)
        {
            this.config = config;
            DataPath = config["DataSettings:DataPath"];
        }
        public RepositoryProvider()
        {
            this.config = Configuration.GetConfig();
            DataPath = config["DataSettings:DataPath"];
        }
        
        public CommentRepository GetCommentRepository(string fileName)
        {
            return new CommentRepository($"{DataPath}\\{fileName}");
        }
        public CommentRepository GetCommentRepository()
            => GetCommentRepository("comments.dat");

        public TaskRepository GetTaskRepository(string fileName)
        {
            return new TaskRepository($"{DataPath}\\{fileName}");
        }
        public TaskRepository GetTaskRepository()
            => GetTaskRepository("tasks.dat");

        public UserRepository GetUserRepository(string fileName)
        {
            return new UserRepository($"{DataPath}\\{fileName}");
        }
        public UserRepository GetUserRepository()
            => GetUserRepository("users.dat");
    }
}
