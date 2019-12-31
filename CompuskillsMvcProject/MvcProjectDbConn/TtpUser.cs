using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
namespace MvcProjectDbConn
{
    public class TtpUser: IdentityUser
    {    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual  ICollection<UserClient> UserClients { get; set; }
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}