
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompuskillsDatabaseProject
{           
    public class EmployeeCredential
        
    {        
               [Key]
        public int EmployeeId { get; set; }
       [ForeignKey("EmployeeId")]
      public virtual Employee Employee { get; set; }
        public ICollection<EmployeeSecurityDevice> EmployeeSecurityDevices { get; set; }
        public Guid SecurityBadgeId { get; set; }  
        public byte Fingerprint { get; set; }
        
    }
}