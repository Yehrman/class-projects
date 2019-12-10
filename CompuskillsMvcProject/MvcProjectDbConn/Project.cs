using System.Collections.Generic;
namespace MvcProjectDbConn
{
    public class Project
    {
        public int ProjectId { get; set; }
        public ICollection<TimeSheetEntry> TimeSheetEntries { get; set; }
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public string TtpUserId { get; set; }
        public virtual TtpUser TtpUser { get; set; }
        public decimal BillRate { get; set; }
        public bool IsActive { get; set; }
    }
}