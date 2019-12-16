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
        [Authorize(Roles ="SystemAdmin")]
        public ActionResult Index()
        {
            var timeSheetEntries = db.TimeSheetEntries.Include(t => t.Project).Include(t => t.TtpUser);
            return View(timeSheetEntries.ToList());
        }
        public ActionResult UserIndex()
        {
            var FindUser = User.Identity.GetUserId();
            var Entries = db.TimeSheetEntries.Where(x => x.TtpUserId == FindUser).Include("Client");
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
               [HttpGet]
     public ActionResult PunchIn()
        {
            var FindUser = User.Identity.GetUserId();
           
            ViewBag.Project = new SelectList(db.Projects.Where(x=>x.TtpUserId==FindUser), "ProjectId", "ProjectName");
          
            return View();

        }
        [HttpPost]
        public ActionResult PunchIn(Project punchInModel)
        {
            if (ModelState.IsValid)
            {
                
                    var project = db.Projects.FirstOrDefault(x => x.ProjectName == punchInModel.ProjectName);
                   // var client = db.Clients.SingleOrDefault(x => x.ClientId==punchInModel.ClientID);
                    var WorkerId = project.TtpUserId;
                    var ProjectId = project.ProjectId;
                    var clientId = project.ClientID;
                    db.TimeSheetEntries.Add(new TimeSheetEntry { TtpUserId = WorkerId, ProjectId = ProjectId, ClientId = clientId, StartTime = DateTime.Now });
                    db.SaveChanges();
                    return RedirectToAction("UserIndex");
                }
            var FindUser = User.Identity.GetUserId();
            ViewBag.Project = new SelectList(db.Projects.Where(x=>x.TtpUserId==FindUser), "ProjectId", "ProjectName",punchInModel.ProjectName );


            return View(punchInModel);
        }
            
        
        [HttpGet]
        public ActionResult PunchOut()
        {
             var FindUser = User.Identity.GetUserId();
           
       

            ViewBag.Project = new SelectList(db.Projects.Where(x => x.TtpUserId == FindUser), "ProjectId", "ProjectName");
          //  ViewBag.ClientName = new SelectList(db.Projects.Where(x => x.TtpUserId == FindUser), "ClientName", "Client.Name");
            return View();
        }
        [HttpPost]
        public ActionResult PunchOut(Project punchOutModel)
        {
            if (ModelState.IsValid)
            {
                if (db.Projects.Any(x => x.ProjectName == punchOutModel.ProjectName))
                {
              
                   var Entry = db.TimeSheetEntries.Include("Project").SingleOrDefault(x=>x.Project.ProjectName == punchOutModel.ProjectName);

                    var entryId = Entry.TimeSheetEntryId;
                    var find = db.TimeSheetEntries.Find(entryId);
                    var end = find.EndTime = DateTime.Now;
                    db.Entry(find).CurrentValues.SetValues(end);
                    db.SaveChanges();
                    return RedirectToAction("UserIndex");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else return View();
            }
        // GET: TimeSheetEntries/Create
      public ActionResult Create()
        {
            string currentUser = User.Identity.GetUserId();
           ViewBag.ProjectId = new SelectList(db.Projects.Where(x=>x.TtpUserId==currentUser), "ProjectId", "ProjectName");
            

            return View();
        }

        // POST: TimeSheetEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeSheetEntryId,TtpUserId,ProjectId,ClientId,StartTime,EndTime")] TimeSheetEntry timeSheetEntry)
        {
            if (ModelState.IsValid)
            {
                db.TimeSheetEntries.Add(timeSheetEntry);
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectId", timeSheetEntry.ProjectId);
            ViewBag.TtpUserId = new SelectList(db.IdentityUsers, "Id", "Email", timeSheetEntry.TtpUserId);
            return View(timeSheetEntry);
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
