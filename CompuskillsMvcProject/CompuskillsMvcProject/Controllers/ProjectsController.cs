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
{
    public class ProjectsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

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

        // GET: Project/Create
        public ActionResult Create()
        {
            var currentUser = User.Identity.GetUserId();
            ViewBag.ClientID = new SelectList(db.UserClients.Include("Client").Where(x=>x.TtpUserId==currentUser), "ClientId", "Client.ClientName");
           
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "ProjectId,ProjectName,TtpUserId,ClientID,BillRate,IsActive")]*/ Project project)
        {
            var currentUser = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Projects.Add(new Project { ProjectName=project.ProjectName,ClientID=project.ClientID,TtpUserId=currentUser,BillRate=project.BillRate,IsActive=true});
                db.SaveChanges();
                var Job = db.Projects.FirstOrDefault(x =>x.ProjectName==project.ProjectName&&x.ClientID==project.ClientID&& x.TtpUserId == currentUser && x.BillRate==project.BillRate&&x.IsActive==true);
                var Id = Job.ProjectId;
                db.WorkScheudules.Add(new WorkSchedule { TtpUserId = currentUser, ProjectId = Id, ClientId = Job.ClientID,Date=DateTime.Today});
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }
              ViewBag.ClientID = new SelectList(db.UserClients.Include("Client").Where(x => x.TtpUserId == currentUser), "ClientId", "Client.ClientName", project.ClientID);
         
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
          //  ViewBag.ClientID = new SelectList(db.Clients, "ClientId", "ClientName", project.ClientID);
     
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,/*[Bind(Include = "ProjectName,BillRate,IsActive")]*/ Project project)
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
        public ActionResult JobSchedule()
        {
            var Schedule = db.WorkScheudules.Include("Project");
            return View(Schedule);
        }
        public ActionResult TodaysJobs()
        {
            var ToDo = db.WorkScheudules.Where(x => x.Date == DateTime.Today);
            return View(ToDo);
        }
        [HttpGet]
        public ActionResult ScheduleJobs()
        {
            var currentUser = User.Identity.GetUserId();
            /// var Job = db.WorkScheudules.Where(x => x.ProjectId == id).OrderByDescending(x => x.id).FirstOrDefault();
            ViewBag.ClientID = new SelectList(db.UserClients.Include("Client").Where(x => x.TtpUserId == currentUser), "ClientId", "Client.ClientName");
            return View();
        }
        [HttpPost]
        public ActionResult ScheduleJobs(int id,Project schedule)
        {


          
            return RedirectToAction("JobSchedule");
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
