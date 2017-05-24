using Microsoft.AspNetCore.Mvc;
using System;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using DbDataProvider;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class HomeController : Controller
    {
        public IRepositoryProvider repositoryProvider 
            => new RepositoryClient().GetRepositoryProvider();
        public IActionResult Index()
        {
            repositoryProvider.GetCommentRepository().GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var context = HttpContext;
            AuthenticationManager.Authenticate(username, password, context);

            if(AuthenticationManager.GetLoggedUser(context) == null)
            {
                ModelState.AddModelError("authenticationFailed", "Wrong username or password!");

                ViewData["username"] = username;

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string firstName, string lastName)
        {
            try
            {
                var user = new User();
                user.UserName = username;
                user.Password = password;
                user.FirstName = firstName;
                user.LastName = lastName;

                var repo = repositoryProvider.GetUserRepository();
                repo.Add(user);
                repo.Save();
                return Login(username, password);
            }
            catch(ArgumentException e)
            {
                ModelState.AddModelError("registrationFailed", e.Message);
                return View();
            }
        }
        
        public IActionResult Logout()
        {
            if(AuthenticationManager.GetLoggedUser(HttpContext) == null)
                return RedirectToAction("Login", "Home");

            AuthenticationManager.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
