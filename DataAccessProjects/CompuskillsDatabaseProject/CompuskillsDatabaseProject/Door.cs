using System.Collections.Generic;
namespace CompuskillsDatabaseProject
{
    public class Door
    {
        public int DoorId { get; set; }

        public ICollection<DoorSecurityDevice> DoorSecurityDevices { get; set; }
        public ICollection<DoorCode> DoorCodes { get; set; }
        public string Room { get; set; }
      
    }
}