using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MvcProjectDbConn;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

using System.Net;

using Microsoft.AspNet.Identity;
using CompuskillsMvcProject.Models;
namespace CompuskillsMvcProject.Models
{

    public class PunchInOutModel
    {
        [Required]
        public string Client { get; set; }

        [Required]
        public string Project { get; set; }
        /*  public void CloseProject()
              {
                using (TimeSheetDbContext db = new TimeSheetDbContext())
                  {
                      var FindUser = HttpContext.Current.User.Identity.GetUserId();
                      TimeSpan time = new TimeSpan(10, 0, 0);
                      var minusTen = DateTime.Now - time;
                      var start = db.TimeSheetEntries.SingleOrDefault(x => x.TtpUserId == FindUser && x.StartTime == minusTen);


                      var end = start.EndTime = DateTime.Now;
                      if (start.EndTime==null)
                      {
                          db.Entry(start).CurrentValues.SetValues(end);
                          db.SaveChanges();
                      }
                  }*/
   
      }
    }

    
   

