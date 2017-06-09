
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Entities
{
    public class Comment : IIdentificatable
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public User Author { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Body { get; set; }
    }
}
