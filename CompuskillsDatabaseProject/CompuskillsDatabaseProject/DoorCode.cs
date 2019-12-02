using System;
using System.ComponentModel.DataAnnotations;

namespace CompuskillsDatabaseProject
{
    public class DoorCode
    {
        [Key]
        public int DoorId { get; set; }
        public virtual Door Door { get; set; }
        public Guid KeyCode { get; set; }
    }
}