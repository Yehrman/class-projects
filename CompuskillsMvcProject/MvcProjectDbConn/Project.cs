using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MvcProjectDbConn
{
    public class Project
    {
        public int ProjectId { get; set; }
        public ICollection<TimeSheetEntry> TimeSheetEntries { get; set; }
        public string ProjectName { get; set; }
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public string TtpUserId { get; set; }
        public virtual TtpUser TtpUser { get; set; }
        [Required]
        public decimal BillRate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}