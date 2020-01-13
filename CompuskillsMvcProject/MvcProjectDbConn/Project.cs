using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MvcProjectDbConn
{
    public class Project
    {
        public int ProjectId { get; set; }
        public ICollection<TimeSheetEntry> TimeSheetEntries { get; set; }
        public ICollection<WorkSchedule> WorkSchedules { get; set; }
        public string ProjectName { get; set; }
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public string TtpUserId { get; set; }
        public virtual TtpUser TtpUser { get; set; }
        [DataType(DataType.Currency)]
        public decimal BillRate { get; set; }      
        public bool IsActive { get; set; }      
    //    public decimal? TotalHours { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalBill { get; set; }
    }
}