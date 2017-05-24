
using System.Collections.Generic;
using Data.Entities.Entities;

namespace DbDataProvider.Repositories
{
    class CommentsRepository : Repository<Comment>
    {
        public CommentsRepository(TaskManagerContext context) : base(context)
        {
        }

        protected override bool IsParent(int id, Comment item)
        {
            return item.TaskId == id;
        }
    }

}
