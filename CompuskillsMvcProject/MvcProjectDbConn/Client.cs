using System.Collections.Generic;
namespace MvcProjectDbConn
{
    public class Client
    {
        public int ClientId { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<TimeSheetEntry> TimeSheetEntries { get; set; }
        public string Name { get; set; }
    }
}