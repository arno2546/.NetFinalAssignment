using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalAssignment.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Column(TypeName = "varchar"), StringLength(100), Display(Name = "Post Title")]
        [Required(ErrorMessage = "Post title cannot be empty")]
        public string PostTitle { get; set; }
        [Column(TypeName = "varchar"), StringLength(300), Display(Name = "Post Text")]
        [Required(ErrorMessage = "Post Text cannot be empty")]
        public string PostText { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}