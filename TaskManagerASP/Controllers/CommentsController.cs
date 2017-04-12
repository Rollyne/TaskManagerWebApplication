using System;
using Microsoft.AspNetCore.Mvc;
using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class CommentsController : Controller
    {
        protected IRepository<Comment> Repository
            => RepositoryProvider.GetRepositoryProvider().GetCommentRepository();

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
            var task = RepositoryProvider
                        .GetRepositoryProvider()
                        .GetTaskRepository()
                        .GetById(item.TaskId);
            int authenticatedId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            if (task.CreatorId == authenticatedId || task.ExecutitiveId == authenticatedId)
                return true;
            ModelState.AddModelError("NoAccess", ErrorMessages.NoAccess("task"));
            return false;
        }

        [HttpPost]
        public IActionResult Edit(Comment item)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            if (HasPermissionToCommentsTask(item))
            {
                try
                {
                    Repository.Update(item);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError("EditFailed", e.Message);
                    ViewData["Comment"] = item;
                }
            }
            else
            {
                return RedirectToAction("Index", "Tasks");
            }

            return RedirectToAction("Details", "Tasks", new {id = item.Id});
        }

        public IActionResult Delete(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            var item = Repository.GetById(id);
            if (!Exists(item) || !HasAccess(item))
                return RedirectToAction("Index", "Tasks");

            Repository.Delete(item);

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
            if (HasPermissionToCommentsTask(item))
            {
                try
                {
                    Repository.Add(item);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError("CreateFailed", e.Message);
                    ViewData["Comment"] = item;
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