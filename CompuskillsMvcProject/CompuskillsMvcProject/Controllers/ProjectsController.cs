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
using CompuskillsMvcProject.Models;
namespace CompuskillsMvcProject.Controllers
{   [Authorize]
    public class ProjectsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();
        [Authorize (Roles ="CEO")]
        public ActionResult Index()
        {
            var Projects = db.Projects.Include("Client");
            return View(Projects);
        }
        // GET: Project
        public ActionResult UserIndex()
        {
            var WorkerId = User.Identity.GetUserId();
            var Jobs = db.Projects.Include("Client").Where(x => x.TtpUserId == WorkerId);
            return View(Jobs);
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.error = id == null;
            Project project = db.Projects.Find(id);
            ViewBag.clientError = project== null;
            return View(project);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var currentUser = User.Identity.GetUserId();
            ViewBag.ClientID = new SelectList(db.UserClients.Include("Client").Where(x => x.TtpUserId == currentUser), "ClientId", "Client.ClientName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateProjectModel projectModel)
        {
            var currentUser = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {             
                db.Projects.Add(new Project { ProjectName = projectModel.ProjectName, ClientID = projectModel.ClientID, TtpUserId = currentUser, BillRate = projectModel.BillRate, IsActive = true });
                db.SaveChanges();
                var job = db.Projects.FirstOrDefault(x => x.ProjectName == projectModel.ProjectName && x.ClientID == projectModel.ClientID && x.TtpUserId == currentUser);
                var id = job.ProjectId;
                db.WorkScheudules.Add(new WorkSchedule { TtpUserId = currentUser, ProjectId = id, ClientId = projectModel.ClientID, Date = projectModel.ScheduleDate });
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }
            ViewBag.ClientID = new SelectList(db.UserClients.Include("Client").Where(x => x.TtpUserId == currentUser), "ClientId", "Client.ClientName", projectModel.ClientID);
            return View(projectModel);
        }
    

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.error = id == null;
            Project project = db.Projects.Find(id);
            ViewBag.clientError = project == null;
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Project project)
        {
            if (ModelState.IsValid)
            {
                var job = db.Projects.FirstOrDefault(x => x.ProjectId == id);
                var user = User.Identity.GetUserId();
                job.TtpUserId = user;
                job.BillRate = project.BillRate;
                job.IsActive = project.IsActive;
                job.ProjectName = project.ProjectName;     
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }
            //ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "ClientName", project.ClientID);
       
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.error = id == null;
            Project project = db.Projects.Find(id);
            ViewBag.clientError = project == null;
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
        
           var schedules = db.WorkScheudules.Where(x => x.ProjectId == id);
       
           foreach (var item in schedules)
            {
                var Id = db.WorkScheudules.Find(item.id);
                db.WorkScheudules.Remove(Id);
            }
            db.SaveChanges();
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
