using System;
using System.Collections.Generic;
using System.Text;

namespace FileDataProvider.Repositories
{
    public interface IRepositoryProvider
    {
        UserRepository GetUserRepository();
        TaskRepository GetTaskRepository();
        CommentRepository GetCommentRepository();
    }
}
