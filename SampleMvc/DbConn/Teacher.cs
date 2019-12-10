using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
namespace DbConn
{
   public class Teacher:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; }
    }
}
