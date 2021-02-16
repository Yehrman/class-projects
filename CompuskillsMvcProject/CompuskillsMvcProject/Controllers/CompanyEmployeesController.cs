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
using Microsoft.AspNet.Identity.EntityFramework;
using CompuskillsMvcProject.Models;
namespace CompuskillsMvcProject.Controllers
{
    [Authorize(Roles = "Ceo,Senior Managment,Human resources department")]
    public class CompanyEmployeesController : Controller
    {
     
        private TimeSheetDbContext db = new TimeSheetDbContext();
        // GET: CompanyEmployees
        public ActionResult Index(string name="")
        {
            var currentUser = User.Identity.GetUserId();
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == currentUser);
            var companyEmployees = db.CompanyEmployees.Include("Employee").Where(x => x.CompanyId == company.CompanyId && x.Name.Contains(name) );   
            return View(companyEmployees);
        }
        [HttpGet]
        public ActionResult Search()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Search(string name)
        {
            return RedirectToAction("Index", new { name });
        }
        [Authorize(Roles ="Ceo")]
        [HttpGet]
        public ActionResult ChangeRole()
        {
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);         
         ViewBag.EmployeeId = new SelectList(db.CompanyEmployees.Include("Employee").Where(x => x.CompanyId == company.CompanyId),"EmployeeId","Employee.FullName");
            var roles = ChangeRoleViewModel.Roles();
            ViewBag.RoleId = new SelectList(roles, "Value", "Key");
            return View();

        }
        [Authorize(Roles = "Ceo")]
        [HttpPost]
        public ActionResult ChangeRole(ChangeRoleViewModel model)
        {
            var user = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                //need to get rid of or edit old entry in userRoles
               var FindUser = db.IdentityUserRoles.SingleOrDefault(x => x.UserId == model.EmployeeId);
                db.IdentityUserRoles.Remove(FindUser);
               // db.SaveChanges();
                db.IdentityUserRoles.Add(new IdentityUserRole { RoleId = model.RoleId, UserId = model.EmployeeId });
                db.SaveChanges();
                //Problem with redirect
                return RedirectToAction("Index");
            }
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);
           ViewBag.EmployeeId = new SelectList(db.CompanyEmployees.Include("Employee").Where(x => x.CompanyId == company.CompanyId), "EmployeeId", "Employee.FullName");
            var roles = ChangeRoleViewModel.Roles();
            ViewBag.RoleId = new SelectList(roles, "Value", "Key");

            return View();
        }
        // GET: CompanyEmployees/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.error = id == null;
            CompanyEmployee companyEmployee = db.CompanyEmployees.Find(id);
            ViewBag.employee = companyEmployee == null;
            return View(companyEmployee);
        }

        // GET: CompanyEmployees/Create
     

        // GET: CompanyEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.error = id == null;
            var user = User.Identity.GetUserId();
            // var compnay=db
            CompanyEmployee companyEmployees = db.CompanyEmployees.Include("Employee").SingleOrDefault(x => x.id == id);
            ViewBag.employee = companyEmployees == null;    
            return View(companyEmployees);
        }

        // POST: CompanyEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CompanyEmployee companyEmployees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyEmployees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", companyEmployees.EmployeeId);
            return View(companyEmployees);
        }
        [HttpGet]
       [Authorize(Roles = "Ceo,Senior Managment")]
        public ActionResult SetPayRate(int? id)
        {
            ViewBag.error = id == null;
            var employee = db.CompanyEmployees.Find(id);
            ViewBag.employee = employee == null;
            return View(employee);
        }
        [HttpPost]
        [Authorize(Roles = "Ceo,Senior Managment")]
        public ActionResult SetPayRate(int id,Utility payment)
        {
            var employee = db.CompanyEmployees.Find(id);
            employee.PayRate = payment.payRate;
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CompanyEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.error = id == null;
            CompanyEmployee companyEmployees = db.CompanyEmployees.Find(id);
            ViewBag.employee = companyEmployees == null;
            return View(companyEmployees);
        }

        // POST: CompanyEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyEmployee companyEmployees = db.CompanyEmployees.Find(id);
            IdentityUserRole userRole = db.IdentityUserRoles.SingleOrDefault(x => x.UserId == companyEmployees.EmployeeId);
            db.IdentityUserRoles.Remove(userRole);
            db.CompanyEmployees.Remove(companyEmployees);
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
