using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class ExersizeApp:DbContext
    {
        public DbSet<Users> GetUsers { get; set; }
        public DbSet<NoGoZones> NoGos { get; set; }
        public DbSet<UsersNoGoZones> GetUsersNos { get; set; }
    }
}