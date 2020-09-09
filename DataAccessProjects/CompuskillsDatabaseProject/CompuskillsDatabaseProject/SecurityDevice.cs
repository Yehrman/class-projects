using System.Collections.Generic;
namespace CompuskillsDatabaseProject
{
    public class SecurityDevice
    {
        public int SecurityDeviceId { get; set; }
        public ICollection<DoorSecurityDevice> DoorSecurityDevices { get; set; }
        public ICollection<EmployeeSecurityDevice> EmployeeSecurityDevices { get; set; }
        public string SecurityDeviceType { get; set; }
     
    }
}