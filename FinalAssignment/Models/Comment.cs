using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalAssignment.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Column(TypeName = "varchar"), StringLength(300), Display(Name = "Comment Text")]
        [Required(ErrorMessage = "Comment cannot be empty")]
        public string CommentText { get; set; }
        public Post Post { get; set; }

    }
}