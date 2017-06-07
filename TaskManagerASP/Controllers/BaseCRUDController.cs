using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using DbDataProvider.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public abstract class BaseCRUDController<TEntity, TIndexViewModel> : Controller
        where TEntity : class, IIdentificatable, new()
        where TIndexViewModel : class, new()
    
    {
        protected IRepository<TEntity> GetRepository()
            => new RepositoryClient().GetRepositoryProvider().GetRepository<TEntity>();

        protected abstract Expression<Func<TEntity, TIndexViewModel>> ViewModelQuery { get; }
        protected abstract TEntity ParseToEntity(TIndexViewModel item);

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
            return item != null;
        }
        protected virtual bool HasAccess(TEntity task)
            => true;

        public virtual IActionResult Index(int? itemsPerPage, int page = 1)
        {
            if (!IsAuthorized())
            {
                ModelState.AddModelError("NoAccess", "You do not have access");
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

            ICollection<TIndexViewModel> model;

            using (var repo = GetRepository())
            {
                model = repo.GetAll<bool, TIndexViewModel>(itemsPerPage: itemsAmount, page: page, where: i => HasAccess(i),
                    select: ViewModelQuery);
                ViewData["PagesAvaliable"] = (int) Math.Ceiling((double) repo.Count() / itemsAmount);
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            if (!IsAuthorized())
            {
                ModelState.AddModelError("NoAccess", "You do not have access");
                return RedirectToAction("Index", "Home");
            }
            TIndexViewModel vm;
            using (var repo = GetRepository())
            {
                vm = repo.FirstOrDefault(i => i.Id == id, select: ViewModelQuery);
            }
            var item = ParseToEntity(vm);
            if (!Exists(item))
                return NotFound();
            if (!HasAccess(item))
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());

            return View(vm);
        }

        [HttpGet]
        public virtual IActionResult Create()
        {
            if (!IsAuthorized())
            {
                ModelState.AddModelError("NoAccess", "You do not have access");
                return RedirectToAction("Index", "Home");
            }

            return View(new TEntity());
        }
        [HttpPost]
        public virtual IActionResult Create(TEntity item)
        {
            if (!IsAuthorized())
            {
                ModelState.AddModelError("NoAccess", "You do not have access");
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                using (var repo = GetRepository())
                {
                    repo.Add(item);
                    repo.Save();
                }
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());
            }
            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAuthorized())
            {
                ModelState.AddModelError("NoAccess", "You do not have access");
                return RedirectToAction("Index", "Home");
            }
            TEntity item;
            using (var repo = GetRepository())
            {
                item = repo.FirstOrDefault(i => i.Id == id);
            }
            if (!Exists(item))
                return NotFound();
            if (!HasAccess(item))
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());

            return View(item);
        }

        [HttpPost]
        public virtual IActionResult Edit(TEntity item)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                using (var repo = GetRepository())
                {
                    repo.Update(item);
                    repo.Save();
                }
                return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());
            }

            return View(item);

        }


        public IActionResult Delete(int id)
        {
            if (!IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }

            TEntity item;
            using (var repo = GetRepository())
            {
                item = repo.FirstOrDefault(i => i.Id == id);

                if (!Exists(item))
                    return NotFound();
                if (!HasAccess(item))
                    return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());

                try
                {
                    repo.Delete(item);
                    repo.Save();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return RedirectToAction("Index", this.ControllerContext.RouteData.Values["controller"].ToString());
        }
    }
}