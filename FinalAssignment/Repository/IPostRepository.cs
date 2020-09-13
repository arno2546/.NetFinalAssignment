using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testFinalAssignment.Repository
{
    interface IPostRepository
    {
        List<Comment> GetPostsWithComments(int id);
    }
}
