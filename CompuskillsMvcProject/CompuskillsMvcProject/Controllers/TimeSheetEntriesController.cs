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
using System.Diagnostics;

namespace CompuskillsMvcProject.Controllers
{     [Authorize]
    public class TimeSheetEntriesController : Controller
    {

        private TimeSheetDbContext db = new TimeSheetDbContext();
  
        [Authorize(Roles = "Ceo,Senior Managment,Finance Department")]
        public ActionResult Index(string name="",DateTime? from=null,DateTime? to =null)
        {
            from = from ?? DateTime.Now.AddDays(-7);
            to = to ?? DateTime.Now;
            var FindUser = User.Identity.GetUserId();
            var Company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == FindUser);
            var Entries = db.TimeSheetEntries.Include("Project").Include("Employee").Where(x => x.CompanyId == Company.CompanyId  && x.StartTime>=from && x.EndTime<=to && x.Project.ProjectName.Contains(name)).OrderByDescending(x => x.StartTime);
            return View(Entries);
        }
        [HttpGet]
        [Authorize(Roles = "Ceo,Senior Managment,Finance Department")]
        public ActionResult Search()
        {
            return PartialView();
        }
        [Authorize(Roles = "Ceo,Senior Managment,Finance Department")]
        [HttpPost]
        public ActionResult Search( DateTime? from = null, DateTime? to = null,string name="")
        {
            return RedirectToAction("Index", new {  from, to,name });
        }
        public ActionResult TimeSheetEntriesForCurrentUser()
        {
            var FindUser = User.Identity.GetUserId();
            var Company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == FindUser);
            var Entries = db.TimeSheetEntries.Include("Project").Where(x => x.EmployeeId == FindUser && x.CompanyId == Company.CompanyId).OrderByDescending(x => x.StartTime);
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
            ViewBag.Error = db.TimeSheetEntries.Any(x => x.EmployeeId == FindUser && x.StartTime <date &&x.StartTime>time&& x.EndTime == null);
            ViewBag.DifferentDate = db.TimeSheetEntries.Any(x => x.EmployeeId == FindUser && x.StartTime <time && x.EndTime == null);
            if (db.TimeSheetEntries.Any(x => x.EmployeeId == FindUser && x.StartTime <date&&x.StartTime>time && x.EndTime == null))
            {
                ModelState.AddModelError("Error", "You never punched out from your last job");               
                return PartialView();
            }
            else
            {
                //what am i loking ffor here
                var project = db.Projects.Find(id);
                Stopwatch timer = Stopwatch.StartNew();
                db.TimeSheetEntries.Add(new TimeSheetEntry { EmployeeId = FindUser,ProjectId=project.ProjectId, StartTime = DateTime.Now,CompanyId=project.CompanyId });
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
            var Entry = db.TimeSheetEntries.FirstOrDefault(x => x.EmployeeId == FindUser && x.StartTime>=time && x.EndTime == null);          
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
        [Authorize(Roles = "Ceo,Senior Managment,Finance Department")]
        public ActionResult DelayedPunchoutIndex()
        {
            var user = User.Identity.GetUserId();
            var empCompany = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);
            DateTime date = DateTime.Now;
            TimeSpan span = new TimeSpan(10, 0, 0);
            DateTime time = date - span;
            var LatePunchouts = db.TimeSheetEntries.Include("Project").Where(x => x.CompanyId == empCompany.CompanyId && x.EndTime == null && x.StartTime < time);
            return View(LatePunchouts);
        }
        [HttpGet]
        [Authorize(Roles = "Ceo,Senior Managment,Finance Department")]
        //check out
        public ActionResult DelayedPunchout(int? id)
        {
            ViewBag.Error = id == null;
            
            DateTime date = DateTime.Now;
            TimeSpan span = new TimeSpan(10, 0, 0);
            DateTime time = date - span;
            var entry = db.TimeSheetEntries.SingleOrDefault(x => x.TimeSheetEntryId == id && x.EndTime==null && x.StartTime< time);
            ViewBag.illegal = entry.EndTime != null || entry.StartTime > time;
            return View(entry);
        }
        [HttpPost]
        [Authorize(Roles = "Ceo,Senior Managment,Finance Department")]
        public ActionResult DelayedPunchout(int id,TimeSheetEntry entry)
        {
                var punchIn = db.TimeSheetEntries.Find(id);
                punchIn.EndTime = entry.EndTime;
                db.Entry(punchIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        
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
