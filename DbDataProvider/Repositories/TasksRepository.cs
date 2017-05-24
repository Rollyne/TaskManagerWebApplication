using System.Collections.Generic;
using Data.Entities.Entities;

namespace DbDataProvider.Repositories
{
    class TasksRepository : Repository<Task>
    {
        public TasksRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
