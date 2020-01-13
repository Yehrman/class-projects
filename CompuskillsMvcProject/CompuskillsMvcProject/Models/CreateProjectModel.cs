using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CompuskillsMvcProject.Models
{
    public class CreateProjectModel
    {
            [Required]
        public string ProjectName { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public decimal BillRate { get; set; }
        [Required,DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd }"), DataType(DataType.Date)]
        public DateTime? ScheduleDate { get; set; }
    } 
}