using System.Collections.Generic;
namespace WebApplication1.Models
{
    public class NoGoZones
    {
        public int id { get; set; }
        public ICollection<UsersNoGoZones> GetNoGoes { get; set; }
        public string Addresses { get; set; }
        public string Laditude { get; set; }
        public string Longitude { get; set; }

    }
}