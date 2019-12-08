using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace MvcProjectDbConn
{
    public class TimeSheetDbContext:DbContext
    {
        public DbSet<TimeSheetEntry> TimeSheetEntries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public static TimeSheetDbContext Create()
        {
            return new TimeSheetDbContext();
        }
    }
}
