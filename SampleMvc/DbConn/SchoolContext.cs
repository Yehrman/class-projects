using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DbConn
{
    public class SchoolContext:IdentityDbContext
    {
        public DbSet<Student> Students { get; set;}
        public DbSet<Grade> Grades { get; set; }
        public static  SchoolContext Create()
        {
            return new SchoolContext();
        }
    }
}
