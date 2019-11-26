
using System.Data.Entity;

namespace CompuskillsDatabaseProject
{
    class BuildingSecurityContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<SecurityDevice> SecurityDevices { get; set; }
        //public DbSet<EmployeeAccessRight> EmployeeAccessRights { get; set; }
        public DbSet<DoorSecurityLevel> DoorSecurityLevels { get; set; }
        public DbSet<DoorSecurityDevice> DoorSecurityDevices { get; set; }
        public DbSet<EmployeeSecurityDevice> EmployeeSecurityDevices { get; set; }
        public DbSet<EmployeeCredential> EmployeeCredentials { get; set; }
        public DbSet<AccessHistory> AccessHistories { get; set; }
       // public DbSet<DoorCode> DoorCodes { get; set; }
    }
}
