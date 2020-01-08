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

namespace CompuskillsMvcProject.Controllers
{
    public class TimeSheetEntriesController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: TimeSheetEntries
       // [Authorize(Roles ="SystemAdmin")]
     //   public ActionResult Index()
     //   {
       //     var timeSheetEntries = db.TimeSheetEntries.Include(t => t.Project).Include(t => t.TtpUser);
          //  return View(timeSheetEntries.ToList());
      //  }
        public ActionResult UserIndex()
        {
            var FindUser = User.Identity.GetUserId();
            var Entries = db.TimeSheetEntries.Where(x => x.UserId == FindUser).Include("Client").Include("Project");
            return View(Entries);
        }
        // GET: TimeSheetEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheetEntry timeSheetEntry = db.TimeSheetEntries.Find(id);
            if (timeSheetEntry == null)
            {
                return HttpNotFound();
            }
            return View(timeSheetEntry);
        }
        public ActionResult PunchIn(int id)
        {

            var FindUser = User.Identity.GetUserId();
            if (db.TimeSheetEntries.Any(x => x.UserId == FindUser && x.StartTime != null && x.EndTime == null))
            {
                ModelState.AddModelError("Error", "You never punched out from your last job");
                return View();

            }
            else
            {
                var project = db.Projects.Include("Client").FirstOrDefault(x => x.ProjectId == id);
                // var client = db.Clients.SingleOrDefault(x => x.ClientId==punchInModel.ClientID);
                var WorkerId = project.TtpUserId;
                var ProjectId = project.ProjectId;
                var clientId = project.ClientID;
                db.TimeSheetEntries.Add(new TimeSheetEntry { UserId = WorkerId, ProjectId = ProjectId, ClientId = clientId, StartTime = DateTime.Now });
                db.SaveChanges();
                return RedirectToAction("");
            }
        }
        public ActionResult PunchOut(int id)
        {
            var Entry = db.TimeSheetEntries.Include("Project").Include("Client").SingleOrDefault(x => x.ProjectId == id && x.EndTime == null);
            var entryId = Entry.TimeSheetEntryId;
            var find = db.TimeSheetEntries.Find(entryId);
            var end = find.EndTime = DateTime.Now;
            db.Entry(find).CurrentValues.SetValues(end);
            db.SaveChanges();
            return PartialView();

        }


        // GET: TimeSheetEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheetEntry timeSheetEntry = db.TimeSheetEntries.Find(id);
            if (timeSheetEntry == null)
            {
                return HttpNotFound();
            }
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
         [HttpGet]
        public ActionResult FindTimeEntriesByDate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindTimeEntriesByDate(ScheduleJobModel dateModel)
        {
            var FindUser = User.Identity.GetUserId();
            var userDates = db.TimeSheetEntries.Where(x => x.UserId == FindUser && x.StartTime == dateModel.FindDate);
            if (userDates != null)
            {
                TempData["dateModel"] = dateModel;
                return RedirectToAction("GetTimeEntriesByDate");
            }
            else
            {
                ModelState.AddModelError("Dates", "There are no entries on this date");
                return View();
            }

        }
        [HttpGet]
    public ActionResult GetTimeEntriesByDate()
    {
            ScheduleJobModel dateModel = (ScheduleJobModel)TempData["dateModel"];
            var FindUser = User.Identity.GetUserId();
            var Entries = db.TimeSheetEntries.Include("Client").Include("Project").Where(x => x.UserId == FindUser && x.StartTime == dateModel.FindDate);
                     
                       
            return View(Entries);
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
