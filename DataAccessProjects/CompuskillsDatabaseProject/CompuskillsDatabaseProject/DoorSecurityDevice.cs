namespace CompuskillsDatabaseProject
{
    public class DoorSecurityDevice
    {
        public int DoorSecurityDeviceId { get; set; }
        public int DoorId { get; set; }
        public virtual Door Doors { get; set; }
        public int SecurityDeviceId { get; set; }
        public virtual SecurityDevice SecurityDevices { get; set; }
    }
}