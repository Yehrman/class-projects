using System.Collections.Generic;
namespace GeoCode
{
    public class NoGoZone
    {
        
        public int id { get; set; }
        public ICollection<UsersNoGoZone> GetNoGoes { get; set; }
        public string Addresses { get; set; }
        public string Laditude { get; set; }
        public string Longitude { get; set; }

    }
}