
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DbDataProvider
{
    public class TaskManagerContextFactory : IDbContextFactory<TaskManagerContext>
    {
        public TaskManagerContext Create(DbContextFactoryOptions options = null)
        {
            return Create("Data Source=DESKTOP-EVCUSNQ;Initial Catalog=TaskManager;Integrated Security=True");
        }
        private TaskManagerContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                $"{nameof(connectionString)} is null or empty.",
                nameof(connectionString));

            var optionsBuilder =
             new DbContextOptionsBuilder<TaskManagerContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new TaskManagerContext(optionsBuilder.Options);
        }
    }
}
