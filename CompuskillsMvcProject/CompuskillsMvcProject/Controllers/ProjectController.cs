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
