
using Data.Entities.Entities;
using Data.Entities.Repositories;
using DbDataProvider;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class UsersController : BaseCRUDController<User>
    {
        private IRepository<User> repository;
        public UsersController()
        {
            this.repository = new RepositoryClient().GetRepositoryProvider().GetUserRepository();
        }

        protected override IRepository<User> Repository => this.repository;
        protected override bool IsAuthorized()
        {
            if (!AuthenticationManager.GetLoggedUser(HttpContext).IsAdmin)
            {
                ViewData["ErrorMessage"] = ErrorMessages.ShouldBeAdmin;
                return false;
            }
            return base.IsAuthorized();
        }
    }
}