using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcProjectDbConn
{
   public class Company
    {
        public Guid CompanyId { get; set; }
        public ICollection<TtpUser> TtpUsers { get; set; }
        public ICollection<CompanyUserRoles> companyUserRoles { get; set; }
        public string CompanyName { get; set; }
    }
}
