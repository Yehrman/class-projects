
namespace FittnessApp.Models
{
    public class UsersNoGoZone
    {
        public int id { get; set; }
        public int UseresId { get; set; }
        public virtual User GetUsers { get; set; }
        public int DangerZoneId { get; set; }
        public virtual NoGoZones GetZones { get; set; }
    }
}