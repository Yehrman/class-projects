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
        public void PunchOut()
        {

            using (TimeSheetDbContext db = new TimeSheetDbContext())
            {

                TimeSpan span = new TimeSpan(10, 0, 0);
                var End = db.TimeSheetEntries.Where(x => x.EndTime == null && x.StartTime == DateTime.Now - span);
                foreach (var item in End)
                {
                    var Id = db.TimeSheetEntries.Find(item.TimeSheetEntryId);
                    var Stop = Id.EndTime = DateTime.Now;
                    db.Entry(Id).CurrentValues.SetValues(Stop);
                }
                db.SaveChanges();
            }
        }
    }
}
      /*  public List<TimeSheetEntry> Entries(DateTime startTime)
        {
            var FindUser = HttpContext.Current.User.Identity.GetUserId();
            using (TimeSheetDbContext db = new TimeSheetDbContext())
            {
                var Entry = db.TimeSheetEntries.Include("Client").Include("Project").Where(x => x.TtpUserId == FindUser && x.StartTime == startTime);
                return Entry.ToList();
            }
            
        }
    }
}*/