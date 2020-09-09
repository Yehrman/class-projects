using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CompuskillsDatabaseProject
{
    public class AccessHistory
    {
        public int AccessHistoryID { get; set; }
        public int DoorID { get; set; }
        public virtual Door Door { get; set; }
        public DateTime AttemptDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employees{ get; set; }
        public bool Result { get; set; }
    }
}