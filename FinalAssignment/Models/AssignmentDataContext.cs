using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalAssignment.Models
{
    public class AssignmentDataContext: DbContext
    {
        public AssignmentDataContext()
        {
            Database.SetInitializer<AssignmentDataContext>(new DropCreateDatabaseIfModelChanges<AssignmentDataContext>());
        }
        virtual public DbSet<Post> Posts { get; set; }
        virtual public DbSet<Comment> Comments { get; set; }
    }
}