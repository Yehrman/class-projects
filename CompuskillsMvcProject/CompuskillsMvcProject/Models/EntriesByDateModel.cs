using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcProjectDbConn;
using Microsoft.AspNet.Identity;
namespace CompuskillsMvcProject.Models
{
    public class EntriesByDateModel
    {
        public string ClientName { get; set; }
         public string ProjectName { get; set; }
        public DateTime? StartTime { get; set; }
         public DateTime? EndTime { get; set; }

        public List<TimeSheetEntry> Entries(DateTime startTime)
        {
            var FindUser = HttpContext.Current.User.Identity.GetUserId();
            using (TimeSheetDbContext db = new TimeSheetDbContext())
            {
                var Entry = db.TimeSheetEntries.Include("Client").Include("Project").Where(x => x.UserId == FindUser && x.StartTime == startTime);
                return Entry.ToList();
            }
            
        }
    }
}