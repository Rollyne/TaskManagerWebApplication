
using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class UsersController : BaseCRUDController<User>
    {
        protected override IRepository<User> Repository 
            => RepositoryProvider.GetRepositoryProvider().GetUserRepository();

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