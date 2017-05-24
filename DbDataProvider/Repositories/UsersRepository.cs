using Data.Entities.Entities;

namespace DbDataProvider.Repositories
{
    class UsersRepository : Repository<User>
    {
        public UsersRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
