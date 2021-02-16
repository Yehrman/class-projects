using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectDbConn
{
  public  class Chat
    {
        public int id { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [ForeignKey("Employee")]
        public string SenderId { get; set; }
        public virtual Employee Employee { get; set; }
        [ForeignKey("EmployeeReciever")]
        public string RecieverId { get; set; }
        public virtual Employee EmployeeReciever { get; set; }
        [Display(Name ="chat message")]
        [Required]
        public string Message { get; set; }
    }
}
