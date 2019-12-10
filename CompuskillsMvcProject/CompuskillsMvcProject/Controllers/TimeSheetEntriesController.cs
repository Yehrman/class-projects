using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcProjectDbConn;

namespace CompuskillsMvcProject.Controllers
{
    public class TimeSheetEntriesController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: TimeSheetEntries
        public ActionResult Index()
        {
            var timeSheetEntries = db.TimeSheetEntries.Include(t => t.Project).Include(t => t.TtpUser);
            return View(timeSheetEntries.ToList());
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

        // GET: TimeSheetEntries/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectId");
            ViewBag.TtpUserId = new SelectList(db.IdentityUsers, "Id", "Email");
            return View();
        }

        // POST: TimeSheetEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeSheetEntryId,TtpUserId,ProjectId,StartTime,EndTime")] TimeSheetEntry timeSheetEntry)
        {
            if (ModelState.IsValid)
            {
                db.TimeSheetEntries.Add(timeSheetEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectId", timeSheetEntry.ProjectId);
            ViewBag.TtpUserId = new SelectList(db.IdentityUsers, "Id", "Email", timeSheetEntry.TtpUserId);
            return View(timeSheetEntry);
        }

        // GET: TimeSheetEntries/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectId", timeSheetEntry.ProjectId);
            ViewBag.TtpUserId = new SelectList(db.IdentityUsers, "Id", "Email", timeSheetEntry.TtpUserId);
            return View(timeSheetEntry);
        }

        // POST: TimeSheetEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeSheetEntryId,TtpUserId,ProjectId,StartTime,EndTime")] TimeSheetEntry timeSheetEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeSheetEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
