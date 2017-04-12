using Microsoft.AspNetCore.Mvc;
using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using System;
using TaskManagerASP.Tools;

namespace TaskManagerASP.Controllers
{
    public class CommentsController : BaseCRUDController<Comment>
    {
        protected override IRepository<Comment> Repository 
            => RepositoryProvider.GetRepositoryProvider().GetCommentRepository();

        public IActionResult Index()
        {
            return View();
        }
    }
}