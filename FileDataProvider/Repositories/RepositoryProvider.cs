using System;
using TaskManagerASP.Configuration;

namespace FileDataProvider.Repositories
{
    public class RepositoryProvider : IRepositoryProvider
    {
        public CommentRepository GetCommentRepository()
        {
            return new CommentRepository(DataSettings.DataPath + "\\comments.dat");
        }

        public TaskRepository GetTaskRepository()
        {
            return new TaskRepository(DataSettings.DataPath + "\\tasks.dat");
        }

        public UserRepository GetUserRepository()
        {
            return new UserRepository(DataSettings.DataPath + "\\users.dat");
        }
    }
}
