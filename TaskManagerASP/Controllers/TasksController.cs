using FileDataProvider.Entities;
using Microsoft.AspNetCore.Mvc;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class TasksController : Controller
    {
        private bool Exists(Task task)
        {
            if(task == null)
            {
                ModelState.AddModelError("DoesNotExist", ErrorMessages.DoesNotExist("task"));
                return false;
            }
            return true;
        }
        private bool HasAccess(Task task)
        {
            int parentId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            if (task.CreatorId != parentId || task.ExecutitiveId != parentId)
            {
                ModelState.AddModelError("NoAccess", ErrorMessages.NoAccess("task"));
                return false;
            }
            return true;
        }
        public IActionResult Index()
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ViewBag.NotLoggedMessage = ErrorMessages.NotAuthenticatedUser;
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Tasks = RepositoryProvider
                                .GetRepositoryProvider()
                                .GetTaskRepository()
                                .GetAll(AuthenticationManager.GetLoggedUser(HttpContext).Id);
            return View();
        }

        public IActionResult Details(int id)
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ViewBag.NotLoggedMessage = ErrorMessages.NotAuthenticatedUser;
                return RedirectToAction("Login", "Home");
            }

            var task = RepositoryProvider
                                .GetRepositoryProvider()
                                .GetTaskRepository()
                                .GetById(id);
            if(!Exists(task) || !HasAccess(task))
                return RedirectToAction("Index", "Tasks");

            ViewBag.Task = task;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ViewBag.NotLoggedMessage = ErrorMessages.NotAuthenticatedUser;
                return RedirectToAction("Login", "Home");
            }
            var task = new Task();
            task.CreatorId = AuthenticationManager.GetLoggedUser(HttpContext).Id;

            ViewBag.Task = task;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Task task)
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ViewBag.NotLoggedMessage = ErrorMessages.NotAuthenticatedUser;
                return RedirectToAction("Login", "Home");
            }

            RepositoryProvider
                        .GetRepositoryProvider()
                        .GetTaskRepository()
                        .Save(task);
            return RedirectToAction("Index", "Contacts");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ViewBag.NotLoggedMessage = ErrorMessages.NotAuthenticatedUser;
                return RedirectToAction("Login", "Home");
            }

            var task = RepositoryProvider
                            .GetRepositoryProvider()
                            .GetTaskRepository()
                            .GetById(id);

            if (!Exists(task) || !HasAccess(task))
                return RedirectToAction("Index", "Tasks");


            ViewBag.Task = task;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Task task)
        {
            return Create(task);
        }

        public IActionResult Delete(int id)
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ViewBag.NotLoggedMessage = ErrorMessages.NotAuthenticatedUser;
                return RedirectToAction("Login", "Home");
            }

            var repository = RepositoryProvider
                        .GetRepositoryProvider()
                        .GetTaskRepository();
            var task = repository.GetById(id);
            if(!Exists(task) || !HasAccess(task))
                return RedirectToAction("Index", "Tasks");

            repository.Delete(task);
         
            return RedirectToAction("Index", "ContactsManager");
        }

    }
}