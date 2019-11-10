using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace GeoCode
{
  public  class FittnesAppContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<NoGoZone> NoGoZones { get; set; }
        public DbSet<UsersNoGoZone> UsersNoZones { get; set; }
    }
}
