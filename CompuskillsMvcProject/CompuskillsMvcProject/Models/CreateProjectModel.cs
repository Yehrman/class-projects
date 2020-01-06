using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcProjectDbConn;
using Microsoft.AspNet.Identity;
namespace CompuskillsMvcProject.Models
{
    public class CreateProjectModel
    {
      public  int ClientId { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Required]
        [Display(Name ="Project Name")]
       public string ProjectName { get; set; }
        [Required]
        [Display (Name ="Bill Rate")]
        public decimal BillRate { get; set; }
      
    }
}