using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcProjectDbConn;
using Microsoft.AspNet.Identity;
using CompuskillsMvcProject.Models;
using System.Timers;
namespace CompuskillsMvcProject.Controllers
{     [Authorize]
    public class TimeSheetEntriesController : Controller
    {

        private TimeSheetDbContext db = new TimeSheetDbContext();
        // GET: TimeSheetEntries
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var timeSheetEntries = db.TimeSheetEntries.Include(t => t.Project).Include(t => t.TtpUser);
           return View(timeSheetEntries.ToList());
        }
        public ActionResult UserIndex()
        {
            var FindUser = User.Identity.GetUserId();
            
            var Entries = db.TimeSheetEntries.Where(x => x.TtpUserId == FindUser).Include("Client").Include("Project");
            return View(Entries);
        }
        // GET: TimeSheetEntries/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.error = id == null;
            TimeSheetEntry timeSheetEntry = db.TimeSheetEntries.Find(id);
            ViewBag.clientError = timeSheetEntry == null;
            return View(timeSheetEntry);
        }
        public ActionResult PunchIn(int id)
        {
            var FindUser = User.Identity.GetUserId();
            DateTime date = DateTime.Now;
            TimeSpan span = new TimeSpan(16, 0, 0);
            DateTime time = date - span;
            ViewBag.Error = db.TimeSheetEntries.Any(x => x.TtpUserId == FindUser && x.StartTime <date &&x.StartTime>time&& x.EndTime == null);
            ViewBag.DifferentDate = db.TimeSheetEntries.Any(x => x.TtpUserId == FindUser && x.StartTime <time && x.EndTime == null);
            if (db.TimeSheetEntries.Any(x => x.TtpUserId == FindUser && x.StartTime <date&&x.StartTime>time && x.EndTime == null))
            {
                ModelState.AddModelError("Error", "You never punched out from your last job");               
                return PartialView();
            }
            else
            {
                var project = db.Projects.FirstOrDefault(x => x.ProjectId == id);
                var ProjectId = project.ProjectId;
                var clientId = project.ClientID;
                db.TimeSheetEntries.Add(new TimeSheetEntry { TtpUserId = FindUser, ProjectId = ProjectId, ClientId = clientId, StartTime = DateTime.Now });
                db.SaveChanges();
                return PartialView();
            }
        }
        public ActionResult PunchOut()
        {
            var FindUser = User.Identity.GetUserId();
           
            DateTime date = DateTime.Now;
            TimeSpan span = new TimeSpan(10, 0, 0);
            DateTime time = date - span;
            var Entry = db.TimeSheetEntries.FirstOrDefault(x => x.TtpUserId == FindUser && x.StartTime>=time && x.EndTime == null);          
            var start = Entry.StartTime;        
            ViewBag.Error = DateTime.Now - start > span;
            if (DateTime.Now - start < span)
            {
                var end = Entry.EndTime = DateTime.Now;
                db.Entry(Entry).CurrentValues.SetValues(end);
                db.SaveChanges();
                return View();
            }
          
            else
            { 
                ModelState.AddModelError("punchout", "The system does'nt handle such long work interval's please contact your administartor");
                return View();
            }
        }
  

        // GET: TimeSheetEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.error = id == null;
            TimeSheetEntry timeSheetEntry = db.TimeSheetEntries.Find(id);
            ViewBag.clientError = timeSheetEntry == null;
            return View(timeSheetEntry);
         
        }

        // POST: TimeSheetEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheetEntry timeSheetEntry = db.TimeSheetEntries.Find(id);
            db.TimeSheetEntries.Remove(timeSheetEntry);
            db.SaveChanges();
            return RedirectToAction("UserIndex");
        }
    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
