using System;

namespace MvcProjectDbConn
{
    public class WorkSchedule
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public virtual TtpUser TtpUser { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public DateTime Date { get; set; }
    }
}