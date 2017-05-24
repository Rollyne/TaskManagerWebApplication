using Microsoft.AspNetCore.Mvc;
using System;
using Data.Entities.Entities;
using Data.Entities.Repositories;
using DbDataProvider;
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