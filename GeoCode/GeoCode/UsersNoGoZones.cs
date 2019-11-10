using System.Collections.Generic;
namespace GeoCode
{
    public class UsersNoGoZone
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public virtual User Users { get; set; }
        public int NoZonesId { get; set; }
        public virtual NoGoZone NoGoZones { get; set; }
    }
}