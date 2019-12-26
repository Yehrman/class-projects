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
    public class ProjectController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: Project
        [Authorize(Roles ="SystemAdmin")]
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.Client).Include(p => p.TtpUser);
            return View(projects.ToList());
        }
        public ActionResult UserIndex()
        {
            var FindUser = User.Identity.GetUserId();
            var UserClients = db.Projects.Where(x => x.TtpUserId == FindUser).Include("Client");
            return View(UserClients);
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        public ActionResult ProjectEntries(int id)
        {
            var userId = User.Identity.GetUserId();
            var Entries = db.TimeSheetEntries.Include("Project").Include("Client").Where(x => x.TtpUserId == userId && x.ProjectId == id);
            return PartialView(Entries);
        }
        /*  [HttpGet]
          public ActionResult PunchIn(int id)
          {
              var project = db.Projects.Include("Client").FirstOrDefault(x => x.ProjectId == id);
              return PartialView(project);
          }
  */      // [HttpPost]
        public ActionResult PunchIn(int id)
        {

            var FindUser = User.Identity.GetUserId();
            if (db.TimeSheetEntries.Any(x => x.TtpUserId == FindUser && x.StartTime != null && x.EndTime == null))
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
                db.TimeSheetEntries.Add(new TimeSheetEntry { TtpUserId = WorkerId, ProjectId = ProjectId, ClientId = clientId, StartTime = DateTime.Now });
                db.SaveChanges();
                return PartialView();
            }
        }
               
            

        


       /* [HttpGet]
        public ActionResult PunchOut()
        {
            var FindUser = User.Identity.GetUserId();
            var Entries = db.TimeSheetEntries.FirstOrDefault(x => x.TtpUserId == FindUser && x.EndTime == null);
            return PartialView(Entries);
        }*/
      // [HttpPost]
        public ActionResult PunchOut(int id)
        {
           
               // if (db.Projects.Any(x => x.ProjectName == punchOutModel.Project && x.Client.Name == punchOutModel.Client))
               // {

                    var Entry = db.TimeSheetEntries.Include("Project").Include("Client").SingleOrDefault(x => x.ProjectId==id&& x.EndTime == null);
                    var entryId = Entry.TimeSheetEntryId;
                    var find = db.TimeSheetEntries.Find(entryId);
                    var end = find.EndTime = DateTime.Now;
                    db.Entry(find).CurrentValues.SetValues(end);
                    db.SaveChanges();
                    return PartialView();
              //  }
                //else
               // {
                 //   ModelState.AddModelError("client,project", "The client or project does'nt exist in the database");
               // }
           
        }
        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "Name");
          //  ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectName,ClientID,TtpUserId,BillRate,IsActive")] Project project)
        {
            if (ModelState.IsValid)
            {
                if (db.Projects.All(x => x.ProjectName != project.ProjectName))
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("UserIndex");
                }
                else
                {
                    ModelState.AddModelError("error", "The project name is taken,Please select a new 1");
                }
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "Name", project.ClientID);
            ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", project.TtpUserId);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "Name", project.ClientID);
            ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", project.TtpUserId);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ClientID,TtpUserId,BillRate,IsActive")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "Name", project.ClientID);
            ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", project.TtpUserId);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
                
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
