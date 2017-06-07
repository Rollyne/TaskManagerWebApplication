using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Data.Entities.Entities;
using Microsoft.AspNetCore.Http;
using TaskManagerASP.Models;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class TasksController : BaseCRUDController<Task, TaskIndexViewModel>
    {


        protected override Expression<Func<Task, TaskIndexViewModel>> ViewModelQuery
        {
            get
            {
                return t => new TaskIndexViewModel()
                {
                    Id = t.Id,
                    Header = t.Header,
                    Description = t.Description,
                    CreatorName = $"{t.Creator.FirstName} {t.Creator.LastName}",
                    ExecutitiveName = $"{t.Executitive.FirstName} {t.Executitive.LastName}",
                    RequiredHours = t.RequiredHours,
                    CreatedOn = t.CreatedOn,
                    LastEditedOn = t.LastEditedOn,
                    IsCompleted = t.IsCompleted
                };
            }
        }

        protected override Task ParseToEntity(TaskIndexViewModel item)
        {
            return new Task()
            {
                Id = item.Id,
                Header = item.Header,
                Description = item.Description,
                CreatorId = item.CreatorId,
                ExecutitiveId = item.ExecutitiveId,
                RequiredHours = item.RequiredHours,
                CreatedOn = item.CreatedOn,
                LastEditedOn = item.LastEditedOn,
                IsCompleted = item.IsCompleted
            };
        }
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
        public IActionResult Index( int? itemsPerPage,
                                    string search = null,
                                    bool description = false,
                                    bool header = false,
                                    int page = 1,
                                    string sort = null)
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



            
            TaskOrderOptions sortOption = TaskOrderOptions.HeaderAsc;
            
            if(sort != null)
                try
                {
                    sortOption = (TaskOrderOptions) Enum.Parse(typeof(TaskOrderOptions), sort);
                }
                catch (Exception)
                {
                    // ignored
                }

            Expression<Func<Task, dynamic>> order = null;
            var descending = false;
            switch (sortOption)
            {
                case TaskOrderOptions.HeaderAsc:
                    order = i => i.Header;
                    break;
                case TaskOrderOptions.HeaderDesc:
                    order = i => i.Header;
                    descending = true;
                    break;
                case TaskOrderOptions.DescriptionDesc:
                    order = i => i.Description;
                    descending = true;
                    break;
                case TaskOrderOptions.DescriptionAsc:
                    order = i => i.Description;
                    break;
                case TaskOrderOptions.RequiredHoursAsc:
                    order = i => i.RequiredHours;
                    break;
                case TaskOrderOptions.RequiredHoursDesc:
                    order = i => i.RequiredHours;
                    descending = true;
                    break;
            }
            ICollection<TaskIndexViewModel> result = new List<TaskIndexViewModel>();
            using (var repo = base.GetRepository())
            {
                result =
                    repo.GetAll(
                        where: i =>
                            (header
                                ? i.Header.StartsWith(search)
                                : i.Description.StartsWith(search))
                            && this.HasAccess(i),
                        orderByKeySelector: order,
                        descending: descending,
                        page: page,
                        itemsPerPage: itemsAmount,
                        select: this.ViewModelQuery
                        );
            }


            ViewData["PagesAvaliable"] = (int)Math.Ceiling((double)result.Count / itemsAmount);
            
            return View(result);
        }
        [HttpPost]
        public override IActionResult Create(Task task)
        {
            task.CreatedOn = DateTime.Now;
            task.LastEditedOn = DateTime.Now;
            task.CreatorId = AuthenticationManager.GetLoggedUser(HttpContext).Id;

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