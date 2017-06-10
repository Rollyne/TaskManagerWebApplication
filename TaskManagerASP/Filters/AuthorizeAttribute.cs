using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagerASP.Models;

namespace TaskManagerASP.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        public bool ShouldBeAdmin { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerContext = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerContext.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute)).Any())
                return;

            var user = AuthenticationManager.GetLoggedUser(context.HttpContext);

            if (user != null)
                if (!ShouldBeAdmin)
                    return;
                else if (user.IsAdmin)
                    return;
            context.Result = new RedirectToActionResult("Login", "Home", new { });
        }
    }
}
