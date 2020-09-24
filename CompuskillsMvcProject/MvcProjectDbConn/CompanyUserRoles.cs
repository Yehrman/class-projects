using System;
using Microsoft.AspNet.Identity.EntityFramework;
namespace MvcProjectDbConn
{
    public class CompanyUserRoles:IdentityUserRole
    {
       
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
