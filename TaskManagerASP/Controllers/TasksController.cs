using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using Microsoft.AspNetCore.Http;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class TasksController : BaseCRUDController<Task>
    {
        private IRepository<Task> repository;
        public TasksController()
        {
            this.repository = new RepositoryClient().GetRepositoryProvider().GetTaskRepository();
        }

        protected override IRepository<Task> Repository => this.repository;

        protected override bool HasAccess(Task task)
        {
            int parentId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            if (task.CreatorId == parentId || task.ExecutitiveId == parentId)
            {
                return true;
            }
            ViewData["ErrorMessage"] = ErrorMessages.NoAccess("task");
            return false;
           
        }

        [HttpGet]
        public IActionResult Index( int? itemsPerPage, string search = null, bool description = false, bool header = false, int page = 1)
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

            ICollection<Task> items = 
                Repository.GetAll(AuthenticationManager.GetLoggedUser(HttpContext).Id);
            ICollection<Task> result = new List<Task>();
            if (header || description)
            {
                if (header)
                    result = items.Where(i => ((i.Header.StartsWith(search ?? "") || string.IsNullOrEmpty(search)))).ToList();
                if (description)
                    foreach (
                        var item in
                        items.Where(i => (i.Description.StartsWith(search ?? "") || string.IsNullOrEmpty(search))))
                    {
                        result.Add(item);
                    }
            }
            else
            {
                result = items;
            }
                        

            ViewData["PagesAvaliable"] = (int)Math.Ceiling((double)result.Count / itemsAmount);

            result = result.Skip((page - 1) * itemsAmount)
                .Take(itemsAmount)
                .ToList();

            ViewData[$"Tasks"] = result;
            ViewData["CurrentPage"] = page;
            
            return View();
        }

        [HttpGet]
        public override IActionResult Create()
        {
            var task = new Task();

            task.CreatorId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            ViewData["Task"] = task;

            return base.Create();
        }
        [HttpPost]
        public override IActionResult Create(Task task)
        {
            task.CreatedOn = DateTime.Now;
            task.LastEditedOn = DateTime.Now;

            return base.Create(task);
        }

        [HttpPost]
        public override IActionResult Edit(Task task)
        {
            task.LastEditedOn = DateTime.Now;

            return base.Edit(task);
        }
    }
}