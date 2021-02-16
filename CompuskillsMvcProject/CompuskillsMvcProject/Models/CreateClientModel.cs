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

        [Display(Name = "Client's Name")]
        public string ClientName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ClientEmail { get; set; }
        [Display(Name = "Clients Phone number ")]
        [DataType(DataType.PhoneNumber)]
        public string ClientPhoneNumber { get; set; }
        [Display(Name ="Clients Address (optional)")]
        public string ClientAddress { get; set; }
        public bool BillByClient { get; set; }
        public bool BillByProject { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name ="Client bill rate")]
        public decimal? BillRate { get; set; }
    }
}