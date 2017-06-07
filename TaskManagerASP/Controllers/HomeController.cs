using Microsoft.AspNetCore.Mvc;
using System;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using DbDataProvider;
using TaskManagerASP.Models;
using TaskManagerASP.Services;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel item)
        {
            var context = HttpContext;
            AuthenticationManager.Authenticate(item.Username, item.Password, context);

            if(AuthenticationManager.GetLoggedUser(context) == null)
            {
                ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
                

                return View(item);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Register(User item)
        {
            if (ModelState.IsValid)
            {
                new AuthenticationService().RegisterUser(item);
                return Login(new LoginViewModel()
                {
                    Username = item.UserName,
                    Password = item.Password
                });
            }
            return View(item);
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
