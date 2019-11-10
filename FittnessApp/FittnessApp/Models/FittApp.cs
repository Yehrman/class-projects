using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FittnessApp.Models
{
    public class FittApp:DbContext
    {
        public DbSet<User> GetUsers { get; set; }
        public DbSet<NoGoZones> NoGos { get; set; }
        public DbSet<UsersNoGoZone> GetUsersNos { get; set; }
    }
}