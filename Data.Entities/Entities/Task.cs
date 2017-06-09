using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Entities
{
    public class Task : IIdentificatable
    {
        private ICollection<Comment> comments;

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Header{get; set; }

        public string Description { get; set; }

        public int RequiredHours { get; set; }

        [Required]
        [ForeignKey("Executitive")]
        public int ExecutitiveId { get; set; }
        public virtual User Executitive { get; set; }

        [Required]
        [ForeignKey("Creator")]
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastEditedOn { get; set; }

        public bool IsCompleted { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
        }
    }
}
