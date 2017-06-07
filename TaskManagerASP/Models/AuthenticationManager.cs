
using Data.Entities.Entities;
using Microsoft.AspNetCore.Http;
using TaskManagerASP.Services;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Models
{
    public static class AuthenticationManager
    {
        public static User GetLoggedUser(HttpContext httpContext)
        {
            AuthenticationService authenticationService = null;
            if (httpContext != null && httpContext.Session.GetObjectFromJson<AuthenticationService>("LoggedUser") == null)
                httpContext.Session.SetObjectAsJson("LoggedUser", new AuthenticationService());

            authenticationService = httpContext.Session.GetObjectFromJson<AuthenticationService>("LoggedUser");
            return authenticationService.LoggedUser;
        }

        public static void Authenticate(string username, string password, HttpContext httpContext)
        {
            AuthenticationService authenticationService = null;

            if (httpContext != null && httpContext.Session.GetObjectFromJson<AuthenticationService>("LoggedUser") == null)
                httpContext.Session.SetObjectAsJson("LoggedUser", new AuthenticationService());

            authenticationService = httpContext.Session.GetObjectFromJson<AuthenticationService>("LoggedUser");
            authenticationService.AuthenticateUser(username, password);

            httpContext.Session.SetObjectAsJson("LoggedUser", authenticationService);
        }
        public static void Logout(HttpContext httpContext)

        {
            httpContext.Session.SetObjectAsJson("LoggedUser", null);
        }
    }
}
