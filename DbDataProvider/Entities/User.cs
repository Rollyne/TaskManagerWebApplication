
using System.Collections.Generic;

namespace DbDataProvider.Entities
{
    public class User : IIdentificatable
    {
        private ICollection<Comment> comments;
        private ICollection<Task> tasksToDo;
        private ICollection<Task> tasksCreated;

        public User()
        {
            comments = new HashSet<Comment>();
            tasksToDo = new HashSet<Task>();
            tasksCreated = new HashSet<Task>();
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public ICollection<Task> TasksToDo
        {
            get { return tasksToDo; }
            set { tasksToDo = value; }
        }

        public ICollection<Task> TasksCreated
        {
            get { return tasksCreated; }
            set { tasksCreated = value; }
        }
    }
}
