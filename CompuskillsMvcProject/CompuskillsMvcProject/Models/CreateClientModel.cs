using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompuskillsMvcProject.Models
{
    public class CreateClientModel
    {
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string ClientEmail { get; set; }
    }
}