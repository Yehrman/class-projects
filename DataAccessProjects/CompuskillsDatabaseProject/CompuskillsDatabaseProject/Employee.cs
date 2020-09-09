using System.Collections.Generic;
namespace CompuskillsDatabaseProject
{
    public class Employee
    {
        public int EmployeeId { get; set; }
     //   public ICollection<EmployeeAccessRight> EmployeeAccessRights { get; set; }
        public ICollection<EmployeeSecurityDevice> EmployeeSecurityDevices { get; set; }
        public string Name { get; set; }
    }
}