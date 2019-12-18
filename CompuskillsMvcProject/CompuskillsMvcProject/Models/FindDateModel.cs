using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CompuskillsMvcProject.Models
{
    public class FindDateModel
    {
            [UIHint("DateTime")]
        public DateTime? FindDate { get; set; }
    } 
}