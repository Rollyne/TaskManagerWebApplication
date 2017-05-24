
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Entities
{
    public class Comment : IIdentificatable
    {
        public int Id { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public User Author { get; set; }

        public string Body { get; set; }
    }
}
