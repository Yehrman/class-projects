using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CompuskillsMvcProject.Models
{
    public class ScheduleDateModel
    {
        public string TtpUserId { get; set; }
        public int ProjectId { get; set; }
        public int ClientId { get; set; }
            [UIHint("DateTime")]
        public DateTime FindDate { get; set; }
    } 
}