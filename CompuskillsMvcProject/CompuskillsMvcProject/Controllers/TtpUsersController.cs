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
{   [Authorize]
    public class TtpUsersController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: TtpUsers
        [Authorize(Roles ="SystemAdmin")]
        public ActionResult Index()
        {
            return View(db.IdentityUsers.ToList());
        }

        // GET: TtpUsers/Details/5
        [Authorize(Roles = "SystemAdmin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TtpUser ttpUser = db.IdentityUsers.Find(id);
            if (ttpUser == null)
            {
                return HttpNotFound();
            }
            return View(ttpUser);
        }


        // GET: TtpUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TtpUser ttpUser = db.IdentityUsers.Find(id);
            if (ttpUser == null)
            {
                return HttpNotFound();
            }
            return View(ttpUser);
        }

        // POST: TtpUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,FirstName,LastName")] TtpUser ttpUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ttpUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ttpUser);
        }

        // GET: TtpUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TtpUser ttpUser = db.IdentityUsers.Find(id);
            if (ttpUser == null)
            {
                return HttpNotFound();
            }
            return View(ttpUser);
        }

        // POST: TtpUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TtpUser ttpUser = db.IdentityUsers.Find(id);
            db.IdentityUsers.Remove(ttpUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Select()
        {
            return View();
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
