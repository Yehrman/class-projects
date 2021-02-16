using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace MvcProjectDbConn
{
    public class TimeSheetDbContext: IdentityDbContext
    {
        public DbSet<TimeSheetEntry> TimeSheetEntries { get; set; }
        public DbSet<Client> Clients { get; set; }
      
        public DbSet<WorkSchedule> WorkScheudules { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<EmployeePayStub> EmployeePayStubs { get; set; } 
        public DbSet<ClientBill> ClientBills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public static TimeSheetDbContext Create()
        {
            return new TimeSheetDbContext();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ClientProject> ClientProjects { get; set; } 
        public DbSet<DeletedCompany> DeletedCompanies { get; set; }
    
        public DbSet<Chat> Chats { get; set; }
        public DbSet<IdentityUserRole> IdentityUserRoles { get; set; }
       public DbSet<FileDetail> FileDetails { get; set; }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }
        // override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ClientBill>()
        //        .HasOptional<>
        //}
       
    }
}
