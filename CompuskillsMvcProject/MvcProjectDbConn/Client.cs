using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MvcProjectDbConn
{
    public class Client
    {
        public int ClientId { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<TimeSheetEntry> TimeSheetEntries { get; set; }
        public ICollection<UserClient> UserClients { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string ClientEmail { get; set; }
    }
}