
using DbDataProvider.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbDataProvider
{
    public class TaskManagerContext : DbContext, ITaskManagerContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            :base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.TasksToDo)
                .WithOne(t => t.Executitive)
                .HasForeignKey(t => t.ExecutitiveId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasMany(u => u.TasksCreated)
                .WithOne(t => t.Creator)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
