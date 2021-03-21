using System;
using System.ComponentModel.DataAnnotations;

namespace ORM.Entity
{
    public class Comment
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public DateTime Posted { get; set; }
        
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}