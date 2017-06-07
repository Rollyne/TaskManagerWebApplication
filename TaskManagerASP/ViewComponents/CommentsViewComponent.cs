using Data.Entities.Entities;
using Data.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskManagerASP.Tools;

namespace TaskManagerASP.ViewComponents
{
    public class CommentsViewComponent : ViewComponent
    {
        private IRepository<Comment> GetRepository() =>
            new RepositoryClient().GetRepositoryProvider().GetRepository<Comment>();

        public IViewComponentResult Invoke(int parentId)
        {
            using (var repo = GetRepository())
            {
                return View(repo.GetAll(where: i => i.TaskId == parentId));
            }
        }
    }
}
