using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MvcProjectDbConn
{
    public class TimeSheetDbContext: IdentityDbContext
    {
        public DbSet<TimeSheetEntry> TimeSheetEntries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkSchedule> WorkScheudules { get; set; }
        public DbSet<UserClient> UserClients { get; set; }
        public static TimeSheetDbContext Create()
        {
            return new TimeSheetDbContext();
        }

        public System.Data.Entity.DbSet<MvcProjectDbConn.TtpUser> IdentityUsers { get; set; }
    }
}
