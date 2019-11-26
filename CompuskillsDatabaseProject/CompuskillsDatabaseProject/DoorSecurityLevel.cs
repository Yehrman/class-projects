using System.ComponentModel.DataAnnotations;

namespace CompuskillsDatabaseProject
{
    public class DoorSecurityLevel
    {
         [Key]
        public int DoorId { get; set; }
        public virtual Door Doors { get; set; }
        public int DoorSecurityLevelRanking { get; set; }
    
    }
} 