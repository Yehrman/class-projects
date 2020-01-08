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
namespace CompuskillsMvcProject.Controllers
{
    public class WorkSchedulesController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: WorkSchedules
      /*  public ActionResult Index()
        {
            var workScheudules = db.WorkScheudules.Include(w => w.Client).Include(w => w.Project).Include(w => w.TtpUser);
            return View(workScheudules.ToList());
        }*/
        public ActionResult UserIndex()
        {
            var currentUser = User.Identity.GetUserId();
            var schedules = db.WorkScheudules.Include("Client").Include("Project").Where(x => x.TtpUserId == currentUser);
            return View(schedules);
        }
        // GET: WorkSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSchedule workSchedule = db.WorkScheudules.Find(id);
            if (workSchedule == null)
            {
                return HttpNotFound();
            }
            return View(workSchedule);
        }

        // GET: WorkSchedules/Create
        public ActionResult Schedule(int id)
        {
           // var currentUser = User.Identity.GetUserId();
            //ViewBag.ClientId = new SelectList(db.UserClients.Include("Client").Where(x=>x.TtpUserId==currentUser), "ClientId", "Client.ClientName");
            //  ViewBag.ProjectId = new SelectList(db.Projects.Where(x=>x.TtpUserId==currentUser), "ProjectId", "ProjectName");
            //  ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email");
            var Job = db.WorkScheudules.Where(x => x.ProjectId == id).OrderByDescending(x => x.id).FirstOrDefault();
            return View(Job);
        }

        // POST: WorkSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Schedule(int id, WorkSchedule schedule)
        {
           if (ModelState.IsValid)
            { 
                var job = db.WorkScheudules.FirstOrDefault(x => x.ProjectId==id);
                job.Date = schedule.Date;
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }
          
         //   ViewBag.ClientId = new SelectList(db.UserClients.Include("Client").Where(x=>x.TtpUserId==currentUser), "ClientId", "Client.ClientName", workSchedule.ClientId);
           // ViewBag.ProjectId = new SelectList(db.Projects.Where(x=>x.TtpUserId==currentUser), "ProjectId", "ProjectName", workSchedule.ProjectId);
            //ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", workSchedule.TtpUserId);
            return View(schedule);
        }

        // GET: WorkSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSchedule workSchedule = db.WorkScheudules.Find(id);
            if (workSchedule == null)
            {
                return HttpNotFound();
            }
            var currentUser = User.Identity.GetUserId();
          //  ViewBag.ClientId = new SelectList(db.UserClients.Include("Client").Where(x=>x.TtpUserId==currentUser), "ClientId", "Client.ClientName", workSchedule.ClientId);
           // ViewBag.ProjectId = new SelectList(db.Projects.Where(x=>x.TtpUserId==currentUser), "ProjectId", "ProjectName", workSchedule.ProjectId);
           // ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", workSchedule.TtpUserId);
         
            return View(workSchedule);
        }

        // POST: WorkSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "id,TtpUserId,ProjectId,ClientId,Date")]*/ WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                var currentUser = User.Identity.GetUserId();
                var job = db.WorkScheudules.FirstOrDefault(x => x.id == workSchedule.id);
                job.TtpUserId = currentUser;
                job.ProjectId = workSchedule.ProjectId;
                job.ClientId = workSchedule.ClientId;
                job.Date = workSchedule.Date;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
          //  ViewBag.ClientId = new SelectList(db.UserClients.Include("Client").Where(x=>x.TtpUserId==currentUser), "ClientId", "Client.ClientName", workSchedule.ClientId);
            //ViewBag.ProjectId = new SelectList(db.Projects.Where(x=>x.TtpUserId==currentUser), "ProjectId", "ProjectName", workSchedule.ProjectId);
         //   ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", workSchedule.TtpUserId);
            return View(workSchedule);
        }

        // GET: WorkSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSchedule workSchedule = db.WorkScheudules.Find(id);
            if (workSchedule == null)
            {
                return HttpNotFound();
            }
            return View(workSchedule);
        }

        // POST: WorkSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkSchedule workSchedule = db.WorkScheudules.Find(id);
            db.WorkScheudules.Remove(workSchedule);
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
