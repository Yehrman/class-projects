
using System.Collections.Generic;
namespace WebApplication1.Models
{
    public class Users
    {
        public int id { get; set; }
        public ICollection<UsersNoGoZones> GetPeople { get; set; }
        public string Name { get; set; }
    }
}