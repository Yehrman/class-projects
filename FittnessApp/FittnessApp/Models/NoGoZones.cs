using System.Collections.Generic;
namespace FittnessApp.Models
{
    public class NoGoZones
    {
        public int id { get; set; }
        public ICollection<UsersNoGoZone> GetNoGoes { get; set; }
        public string Address { get; set; }
        public string Laditude { get; set; }
        public string Longitude { get; set; }
    }
}