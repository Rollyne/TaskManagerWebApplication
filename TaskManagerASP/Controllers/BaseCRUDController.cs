using System;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

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
                ViewData["ErrorMessage"] = ErrorMessages.DoesNotExist(typeof(TEntity).Name.ToLower());
                return false;
            }
            return true;
        }
        protected virtual bool HasAccess(TEntity task)
            => true;

        public virtual IActionResult Index(int? itemsPerPage, int page = 1)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Login", "Home");
            }
            int itemsAmount;
            if (itemsPerPage != null)
            {
                this.HttpContext.Session.SetInt32("itemsPerPage", itemsPerPage.Value);
                itemsAmount = itemsPerPage.Value;
            }
            else
            {
                itemsAmount = this.HttpContext.Session.GetInt32("itemsPerPage") ?? Constants.DefaultItemsPerPage;
            }


            ViewData[$"{typeof(TEntity).Name}s"] = Repository.GetAmountBySkipping((page-1)*itemsAmount, itemsAmount, AuthenticationManager.GetLoggedUser(HttpContext).Id);
            ViewData["CurrentPage"] = page;
            ViewData["PagesAvaliable"] = (int)Math.Ceiling((double)Repository.Count() / itemsAmount);
            return View();
        }

        public IActionResult Details(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public virtual IActionResult Create(TEntity item)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                Repository.Add(item);
                Repository.Save();
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
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            }

            try
            {
                Repository.Update(item);
                Repository.Save();
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
                return RedirectToAction("Index", "Home");
            }

            var item = Repository.GetById(id);
            if (!Exists(item) || !HasAccess(item))
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());

            try
            {
                Repository.Delete(item);
                Repository.Save();
            }
            catch (Exception)
            {
                // ignored
            }

            return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());
        }
    }
}