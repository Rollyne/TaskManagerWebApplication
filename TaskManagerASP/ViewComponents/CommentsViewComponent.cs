using FileDataProvider.Entities;
using FileDataProvider.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskManagerASP.Tools;

namespace TaskManagerASP.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        protected IRepository<Comment> Repository 
            => RepositoryProvider.GetRepositoryProvider().GetCommentRepository();

        public IViewComponentResult Invoke(int parentId)
        {
            return View(Repository.GetAll(parentId));
        }
    }
}
