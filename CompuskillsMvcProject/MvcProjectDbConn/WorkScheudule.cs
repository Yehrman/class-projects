using System;
using System.ComponentModel.DataAnnotations;

namespace MvcProjectDbConn
{
    public class WorkSchedule
    {
        public int id { get; set; }
        public string TtpUserId { get; set; }
        public virtual TtpUser TtpUser { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd H:mm:ss tt}"), DataType(DataType.DateTime)]
        [UIHint("PickDate")]
        public DateTime? Date { get; set; }
    }
} 