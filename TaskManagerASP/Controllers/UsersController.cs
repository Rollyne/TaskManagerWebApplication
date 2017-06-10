
using System;
using System.Linq.Expressions;
using Data.Entities.Entities;
using TaskManagerASP.Filters;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    [Authorize(ShouldBeAdmin = true)]
    public class UsersController : BaseCRUDController<User, UserIndexViewModel>
    {
        protected override Expression<Func<User, UserIndexViewModel>> ViewModelQuery { get
        {
            return u => new UserIndexViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.UserName,
                IsAdmin = u.IsAdmin
            };
        } }
        protected override User ParseToEntity(UserIndexViewModel item)
        {
            return new User()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                UserName = item.Username,
                IsAdmin = item.IsAdmin
            };
        }
        //protected override bool IsAuthorized()
        //{
        //    if (!AuthenticationManager.GetLoggedUser(HttpContext).IsAdmin)
        //    {
        //        ViewData["ErrorMessage"] = ErrorMessages.ShouldBeAdmin;
        //        return false;
        //    }
        //    return base.IsAuthorized();
        //}
    }
}