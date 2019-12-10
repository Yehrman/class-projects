using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcProjectDbConn;

namespace CompuskillsMvcProject.Controllers
{
    public class ProjectsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            // var projects = db.Projects.Include(p => p.Client);
         //   var user = db.Users.SingleOrDefault(x => x.Email == User.Identity.Name);
            var FindUser = User.Identity.GetUserId();         
            var UserClients = db.Projects.Where(x => x.TtpUserId == FindUser).Include("Client");
            return View(UserClients);
          
        }

        // GET: Projects/Details/5
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

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ClientID,BillRate,IsActive")] Project project )
        {
            if (ModelState.IsValid)
            {
              var user=  User.Identity.GetUserId();
            
                db.Projects.Add(new Project { ProjectId=project.ProjectId,ClientID=project.ClientID,BillRate=project.BillRate,IsActive=project.IsActive,TtpUser=user});
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "Name", project.ClientID);
            return View(project);
        }

        // GET: Projects/Edit/5
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
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ClientID,BillRate,IsActive")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "Name", project.ClientID);
            return View(project);
        }

        // GET: Projects/Delete/5
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

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult  GetJobsByDate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetJobsByDate(DateTime date)
        {
            return RedirectToAction("SeeJobsByDate");
        }
        [HttpGet]
        public ActionResult SeeJobsByDate(DateTime date,Project project)
        {
         //   var update = db.TimeSheetEntries.Where(x => x.StartTime <= date || x.EndTime >= date);
            if(db.TimeSheetEntries.Any(x=>x.StartTime<=date&&x.EndTime>=date))
            {
                return View(project);
            }
            else
            {
                return HttpNotFound();
            }
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
