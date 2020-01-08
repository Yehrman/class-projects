using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CompuskillsMvcProject.Models
{
    public class ScheduleJobModel
    {
            
        public int ProjectName { get; set; }
        public int ClientName { get; set; }
            [UIHint("DateTime")]
        public DateTime FindDate { get; set; }
    } 
}