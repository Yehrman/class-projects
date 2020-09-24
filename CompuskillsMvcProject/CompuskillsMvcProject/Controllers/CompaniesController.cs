using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompuskillsMvcProject.Models;
using Microsoft.AspNet.Identity;
using MvcProjectDbConn;

namespace CompuskillsMvcProject.Controllers
{
    public class CompaniesController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: Companies
        
        //public ActionResult Index()
        //{
        //    return View(db.Companies.ToList());
        //}
        [HttpGet]
        public ActionResult CompanyOfUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CompanyOfUser(CompanyViewModel model)
        {
            var user = User.Identity.GetUserId();
            if (db.Companies.Any(x => x.CompanyName == model.CompanyName))
            {
                var Company = db.Companies.FirstOrDefault(x => x.CompanyName == model.CompanyName);
                var CompanyId = Company.CompanyId;

                var CurrentUser = db.IdentityUsers.FirstOrDefault(x => x.Id == user);
                CompanyId = (Guid)CurrentUser.CompanyId;
                db.Entry(CurrentUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                TtpUser ttpUser = new TtpUser();
                Guid guid = Guid.NewGuid();
                db.Companies.Add(new Company { CompanyId = guid, CompanyName = model.CompanyName });
                db.SaveChanges();                          
                    var Company = db.Companies.FirstOrDefault(x => x.CompanyName == model.CompanyName);
                    var CurrentUser = db.IdentityUsers.SingleOrDefault(x => x.Id == user);
                //I need to insert into ttpUsers the company guid.

             
                CurrentUser.CompanyId = Company.CompanyId;

                 db.Entry(CurrentUser).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Login");
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
