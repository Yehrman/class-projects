using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleMvc.Models
{
    public class Misc
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age{ get; set; }
        public string Residence { get; set; }
    }
}