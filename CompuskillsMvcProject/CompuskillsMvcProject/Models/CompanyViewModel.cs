using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompuskillsMvcProject.Models
{
    public class CompanyViewModel
    {
        [Required]
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        [Display (Name ="Biling Address")]
        public string Address { get; set; }
        [DataType(DataType.Password)]
       public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Currency)]
        public string EmployeePayInterval { get; set; }
        public string ClientBillInterval { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        public List<string> Interval()
        {
            List<string> time = new List<string>();
            time.Add("daily");
            time.Add("weekly");
            time.Add("Every other week");
            time.Add("monthly");
            time.Add("On a per project basis");
            return time;
        }

    }
}