using System.Collections.Generic;
namespace CompuskillsDatabaseProject
{
    public class EmployeeSecurityDevice
    {
       public int EmployeeSecurityDeviceId { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EmployeeCredential EmployeeCredential { get; set; }
        public int SecurityDeviceId { get; set; }
        public virtual SecurityDevice SecurityDevices { get; set; }
   
    }
}