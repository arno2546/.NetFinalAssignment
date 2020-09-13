using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testFinalAssignment.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public List<Comment> GetPostsWithComments(int id)
        {
            return this.context.Comments.Where(x => x.PostId == id).ToList();
        }
    }
}