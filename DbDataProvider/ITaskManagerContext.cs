using DbDataProvider.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbDataProvider
{
    public interface ITaskManagerContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }
    }
}