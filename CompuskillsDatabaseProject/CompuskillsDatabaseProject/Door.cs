using System.Collections.Generic;
namespace CompuskillsDatabaseProject
{
    public class Door
    {
        public int DoorId { get; set; }
        public ICollection<DoorSecurityLevel> DoorSecurityLevels { get; set; }
        public ICollection<DoorSecurityDevice> DoorSecurityDevices { get; set; }
        public string Room { get; set; }
        public string KeyCode { get; set; }
    }
}