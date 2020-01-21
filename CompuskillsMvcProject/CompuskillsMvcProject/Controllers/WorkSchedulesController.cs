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
    [Authorize]
    public class WorkSchedulesController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

       //  GET: WorkSchedules
       [Authorize(Roles ="CEO")]
        public ActionResult Index()
        {
            var workScheudules = db.WorkScheudules.Include(w => w.Client).Include(w => w.Project).Include(w => w.TtpUser);
            return View(workScheudules.ToList());
        }
        //All future scheduled jobs
        public ActionResult UserIndex()
        {
            var currentUser = User.Identity.GetUserId();
            var schedules = db.WorkScheudules.Include("Client").Include("Project").Where(x => x.TtpUserId == currentUser&&x.Date>=DateTime.Today);
            return View(schedules);
        }
        public ActionResult PastSchedule()
        {
            var currentUser = User.Identity.GetUserId();
            var schedules = db.WorkScheudules.Include("Client").Include("Project").Where(x => x.TtpUserId == currentUser && x.Date < DateTime.Today);
            return View(schedules);
        }
        //Future scheduled jobs for 1  job
        public ActionResult JobSchedule(int id)
        {
            var userid = User.Identity.GetUserId();
            var Schedule = db.WorkScheudules.Include("Client").Where(x => x.ProjectId == id && x.TtpUserId == userid && x.Date >= DateTime.Today);
            return View(Schedule);
        }
        public ActionResult TodaysJobs()
        {
            var userid = User.Identity.GetUserId();
            if (User.Identity.IsAuthenticated)
            {
                var ToDo = db.WorkScheudules.Where(x => x.TtpUserId == userid && x.Date == DateTime.Today);
                return View(ToDo);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: WorkSchedules/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.error = id == null;
            WorkSchedule workSchedule = db.WorkScheudules.Find(id);
            ViewBag.clientError = workSchedule == null;
            return View(workSchedule);
        }
        [HttpGet]
        public ActionResult ScheduleJob(int id)
        {
            var CurrentUser = User.Identity.GetUserId();
            var Job=db.WorkScheudules.Where(x => x.ProjectId == id&&x.TtpUserId==CurrentUser).OrderByDescending(x => x.id).FirstOrDefault();
            return View(Job);
        }
        [HttpPost]
        public ActionResult ScheduleJob(int id,WorkSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = User.Identity.GetUserId();
                var Job = db.WorkScheudules.Where(x => x.ProjectId == id && x.TtpUserId == CurrentUser).OrderByDescending(x => x.id).FirstOrDefault();
                if (schedule.Date >= DateTime.Today)
                {
                    db.WorkScheudules.Add(new WorkSchedule { TtpUserId = CurrentUser, ProjectId = Job.ProjectId, ClientId = Job.ClientId, Date = schedule.Date });
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Error", "You're can't schedule for date that already passed.");
                    return View();
                }
                return RedirectToAction("UserIndex");
            }
            return View(schedule);
        }
        [HttpGet]
        public ActionResult ReSchedule(int id)
        {
            var Job = db.WorkScheudules.Find(id);

            if (Job.Date >= DateTime.Today)
            {
                return View(Job);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReSchedule(int id, WorkSchedule schedule)
        {
           if (ModelState.IsValid)
            {  if (schedule.Date >= DateTime.Today)
                {
                    var job = db.WorkScheudules.Find(id);
                    job.Date = schedule.Date;
                    db.SaveChanges();
                    return RedirectToAction("UserIndex");
                }
                else
                {
                    ModelState.AddModelError("Error", "You're can't reschedule for date that already passed.");
                    return View();
                }
            }
            return View(schedule);
        }
  

        // GET: WorkSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.error = id == null;
            WorkSchedule workSchedule = db.WorkScheudules.Find(id);
            ViewBag.clientError = workSchedule == null;
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
