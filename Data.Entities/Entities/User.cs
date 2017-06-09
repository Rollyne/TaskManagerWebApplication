using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Entities
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

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Password { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
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
