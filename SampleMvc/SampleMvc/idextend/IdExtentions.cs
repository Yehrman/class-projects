using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbConn;
using DbConn.IdentityExtensions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace SampleMvc.idextend
{
   
        public static class IdentityExtensions
        {
            public static TeacherUserManager CreateUserManager(this IdentityFactoryOptions<TeacherUserManager> options, IOwinContext context)
            {
                var UserStore = new UserStore<Teacher>(context.Get<SchoolContext>());
                var Manager = TeacherUserManager.Create(UserStore);
                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    Manager.UserTokenProvider =
                        new DataProtectorTokenProvider<Teacher>(dataProtectionProvider.Create("ASP.NET Identity"));
                }

                return Manager;
            }
        
}