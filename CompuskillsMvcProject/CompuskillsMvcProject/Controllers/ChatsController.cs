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
    public class ChatsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();
    
        // GET: Chats
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.FirstOrDefault(x => x.EmployeeId == user);
            var chats = db.Chats.Include("Employee").Where(x => x.SenderId == user ||x.RecieverId==user && x.CompanyId == company.CompanyId);
            return View(chats);
        }

        public ActionResult Create()
        {
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.FirstOrDefault(x => x.EmployeeId == user);
            ViewBag.RecieverId = new SelectList(db.CompanyEmployees.Include("Employee").Where(x=>x.CompanyId==company.CompanyId), "EmployeeId", "Employee.FullName");
            return View();
        }

        // POST: Chats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecieverId,Message")] Chat chat)
        {
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.FirstOrDefault(x => x.EmployeeId == user);
            if (ModelState.IsValid)
            {
                chat.SenderId = user;
                chat.CompanyId = company.CompanyId;
                db.Chats.Add(chat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }     
            ViewBag.RecieverId = new SelectList(db.CompanyEmployees.Include("Employee").Where(x => x.CompanyId == company.CompanyId), "EmployeeId", "Employee.FullName");
            return View(chat);
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
