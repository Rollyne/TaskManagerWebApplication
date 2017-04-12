using System;
using Microsoft.AspNetCore.Mvc;
using FileDataProvider.Repositories;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;
using FileDataProvider.Entities;

namespace TaskManagerASP.Controllers
{
    public abstract class BaseCRUDController<TEntity> : Controller
        where TEntity : IIdentificatable, new()
    {
        protected abstract IRepository<TEntity> Repository { get; }

        protected virtual bool IsAuthorized()
        {
            if (AuthenticationManager.GetLoggedUser(HttpContext) == null)
            {
                ModelState.AddModelError("AccessDenied", ErrorMessages.NotAuthenticatedUser);
                return false;
            }
            return true;
        }

        protected virtual bool Exists(TEntity item)
        {
            if (item == null)
            {
                ModelState.AddModelError("DoesNotExist", ErrorMessages.DoesNotExist(typeof(TEntity).Name.ToLower()));
                return false;
            }
            return true;
        }
        protected virtual bool HasAccess(TEntity task)
            => true;

        public virtual IActionResult Index()
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            ViewData[$"{typeof(TEntity).Name}s"] = Repository.GetAll(AuthenticationManager.GetLoggedUser(HttpContext).Id);

            return View();
        }

        public IActionResult Details(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            var item = Repository.GetById(id);

            if (!Exists(item) || !HasAccess(item))
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());

            ViewData[typeof(TEntity).Name] = item;
            return View();
        }

        [HttpGet]
        public virtual IActionResult Create()
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
        [HttpPost]
        public virtual IActionResult Create(TEntity item)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                Repository.Add(item);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("CreateFailed", e.Message);
                ViewData[typeof(TEntity).Name] = item;
                return View();
            }

            return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            var item = Repository.GetById(id);

            if (!Exists(item) || !HasAccess(item))
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());


            ViewData[typeof(TEntity).Name] = item;
            return View();
        }

        [HttpPost]
        public virtual IActionResult Edit(TEntity item)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                Repository.Update(item);
            }
            catch(ArgumentException e)
            {
                ModelState.AddModelError("EditFailed", e.Message);
                ViewData[typeof(TEntity).Name] = item;
                return View();
            }
            
            return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());
        }


        public IActionResult Delete(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }

            var item = Repository.GetById(id);
            if (!Exists(item) || !HasAccess(item))
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());

            Repository.Delete(item);

            return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());
        }
    }
}