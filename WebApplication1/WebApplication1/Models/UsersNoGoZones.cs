namespace WebApplication1.Models
{
    public class UsersNoGoZones
    {
        public int id { get; set; }
        public int UsersId { get; set; }
        public virtual Users GetUsers { get; set; }
        public int DangerZonesId { get; set; }
        public virtual NoGoZones GetZones { get; set; }
    }
}