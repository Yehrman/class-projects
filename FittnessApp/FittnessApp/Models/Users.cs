using System.Collections.Generic;
namespace FittnessApp.Models
{
    public class User
    {
        public int id { get; set; }
        public ICollection<UsersNoGoZone> GetPeople { get; set; }
        public string Name { get; set; }
    }
}