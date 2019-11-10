using System.Collections.Generic;

namespace GeoCode
{
    public class User
    {
        public int id { get; set; }
        public ICollection<UsersNoGoZone> UsersNoGoZones { get; set; }
        public string Name { get; set; }
    }
}