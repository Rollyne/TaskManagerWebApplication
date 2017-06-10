using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Data.Entities.Entities;
using Microsoft.AspNetCore.Http;
using TaskManagerASP.Filters;
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
                    CreatorId = t.CreatorId,
                    ExecutitiveId = t.ExecutitiveId,
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
            return false;
           
        }

        [Log]
        [HttpGet]
        public IActionResult Index( int? itemsPerPage,
                                    string search = null,
                                    bool description = false,
                                    bool header = false,
                                    int page = 1,
                                    string sort = null)
        {
            //if (!IsAuthorized())
            //{
            //    return RedirectToAction("Login", "Home");
            //}
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
            int parentId = AuthenticationManager.GetLoggedUser(HttpContext).Id;
            Expression<Func<Task, bool>> where = t => (t.CreatorId == parentId || t.ExecutitiveId == parentId);
            if (!string.IsNullOrEmpty(search))
            {
                if (header && description)
                    where = t => t.Header.StartsWith(search) || t.Description.StartsWith(search) && (t.CreatorId == parentId || t.ExecutitiveId == parentId);
                else if (header)
                    where = t => t.Header.StartsWith(search) && (t.CreatorId == parentId || t.ExecutitiveId == parentId);
                else if(description)
                    where = t => t.Description.StartsWith(search) && (t.CreatorId == parentId || t.ExecutitiveId == parentId);
            }
            ICollection<TaskIndexViewModel> model = new List<TaskIndexViewModel>();
            int itemsAvaliable;
            using (var repo = base.GetRepository())
            {
                var result =
                    repo.GetAllPaged(
                        where: where,
                        orderByKeySelector: order,
                        descending: descending,
                        page: page,
                        itemsPerPage: itemsAmount,
                        select: this.ViewModelQuery
                        );
                model = result.Item1;
                itemsAvaliable = result.Item2;
            }

            ViewData["CurrentPage"] = page;
            ViewData["PagesAvaliable"] = (int)Math.Ceiling((double)itemsAvaliable / itemsAmount);
            
            return View(model);
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