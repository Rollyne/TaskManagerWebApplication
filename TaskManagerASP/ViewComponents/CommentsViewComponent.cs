using Data.Entities.Entities;
using Data.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskManagerASP.Tools;

namespace TaskManagerASP.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        private IRepository<Comment> repository;
        public CommentsViewComponent()
        {
            this.repository = new RepositoryClient().GetRepositoryProvider().GetCommentRepository();
        }

        private IRepository<Comment> Repository => this.repository;

        public IViewComponentResult Invoke(int parentId)
        {
            return View(Repository.GetAll(parentId));
        }
    }
}
