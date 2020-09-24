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
        public DbSet<Company> Companies { get; set; }
        public static TimeSheetDbContext Create()
        {
            return new TimeSheetDbContext();
        }

        public DbSet<TtpUser> IdentityUsers { get; set; }
        public DbSet<CompanyUserRoles> IdentityUserRoles { get; set; }
    }
}
