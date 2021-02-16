using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcProjectDbConn
{
    public class Client
    {
        public int ClientId { get; set; }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string ClientName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ClientEmail { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string ClientPhoneNumber { get; set; }
        public string ClientAddress { get; set; }
        public bool BillByClient { get; set; }
        public bool BillByProject { get; set; }
       
     
        [DataType(DataType.Currency)]
        public decimal? BillRate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<ClientBill> ClientBills { get; set; }
        public ICollection<ClientProject> ClientProjects { get; set; }
    }
}