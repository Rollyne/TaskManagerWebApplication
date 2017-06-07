using System;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class CommentsController : Controller
    {
        private IRepository<Comment> GetRepository() =>
            new RepositoryClient().GetRepositoryProvider().GetRepository<Comment>();

        private bool IsAuthorized()
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ModelState.AddModelError("AccessDenied", ErrorMessages.NotAuthenticatedUser);
                return false;
            }
            return true;
        }

        private bool HasPermissionToCommentsTask(Comment item)
        {
            var task = new RepositoryClient()
                        .GetRepositoryProvider()
                        .GetTaskRepository()
                        .FirstOrDefault(where: t => t.Id == item.TaskId);
            int authenticatedId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            if (task.CreatorId == authenticatedId || task.ExecutitiveId == authenticatedId)
                return true;
            ModelState.AddModelError("NoAccess", ErrorMessages.NoAccess("task"));
            return false;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }
            Comment item;
            using (var repo = GetRepository())
            {
                item = repo.FirstOrDefault(i => i.Id == id);
            }
            
            if(!Exists(item))
                return NotFound();

            if (!HasAccess(item))
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Comment item)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            item.AuthorId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            if (HasPermissionToCommentsTask(item))
            {
                if (ModelState.IsValid)
                {
                    using (var repo = GetRepository())
                    {
                        repo.Update(item);
                        repo.Save();
                    }
                }
                else
                {
                    return View(item);
                }
            }
            else
            {
                return RedirectToAction("Index", "Tasks");
            }

            return RedirectToAction("Details", "Tasks", new { id = item.Id });
        }

        public IActionResult Delete(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            Comment item;

            using (var repo = GetRepository())
            {
                item = repo.FirstOrDefault(i => i.Id == id);
                if (Exists(item))
                    return NotFound();
                if (!HasAccess(item))
                    return RedirectToAction("Index", "Tasks");
                if (ModelState.IsValid)
                {
                    repo.Delete(item);
                    repo.Save();
                }
            }
                

            return RedirectToAction("Details", "Tasks", new {id = item.TaskId});
        }

        [HttpPost]
        public IActionResult Create(Comment item)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }
            item.AuthorId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            if (HasPermissionToCommentsTask(item) && ModelState.IsValid)
            {
                using (var repo = GetRepository())
                {
                    repo.Add(item);
                    repo.Save();
                }
            }
            else
            {
                return RedirectToAction("Index", "Tasks");
            }

            return RedirectToAction("Details", "Tasks", new {id = item.TaskId});
        }

        private bool Exists(Comment item)
        {
            if (item == null)
            {
                ModelState.AddModelError("DoesNotExist", ErrorMessages.DoesNotExist("comment"));
                return false;
            }
            return true;
        }

        private bool HasAccess(Comment item)
        {
            if (item.AuthorId != AuthenticationManager.GetLoggedUser(HttpContext).Id)
            {
                ModelState.AddModelError("NoAccess", ErrorMessages.NoAccess("comment"));
                return false;
            }
            return true;
        }
    }
}