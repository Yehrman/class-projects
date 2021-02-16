using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompuskillsMvcProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcProjectDbConn;

namespace CompuskillsMvcProject.Controllers
{
    public class CompaniesController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();
        
        const string Employee = "35FB245B-FDE0-464D-9985-CFDD0F2AEB18";
        const string Ceo = "602514B5-413F-419D-97A1-2533130D8DC4";

                [HttpGet]
        public ActionResult AddCompany()
        {
            CompanyViewModel model = new CompanyViewModel();
            var intervals = model.Interval();
            ViewBag.ClientBillInterval = new SelectList(intervals);
            ViewBag.EmployeePayInterval = new SelectList(intervals);
            return View();
            
        }
        [HttpPost]
        public ActionResult AddCompany(CompanyViewModel company)
        {           
            var user = User.Identity.GetUserId();
            Guid guid = Guid.NewGuid();
            //  company.CompanyId = guid;
            if (db.Companies.All(x => x.Password != company.Password))
            {
                db.Companies.Add(new Company { CompanyId = guid, CompanyName = company.CompanyName, CompanyPhoneNumber = company.PhoneNumber, Email = company.Email, Password = company.Password,ClientBillInterval=company.ClientBillInterval,EmployeePayInterval=company.EmployeePayInterval,DateJoined=DateTime.Today });
                var newCompany = db.Companies.SingleOrDefault(x => x.CompanyId == guid && x.Password == company.Password);
                db.CompanyEmployees.Add(new CompanyEmployee { EmployeeId = user, CompanyId = guid });
                db.IdentityUserRoles.Add(new IdentityUserRole { RoleId = Ceo, UserId = user });
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                //This would be a terrible security leak we would have to send a urgent message to this company to reset there password 
                ModelState.AddModelError("password", "That password is invalid please add a new password");
             //  var duplicate= db.Companies.FirstOrDefault(x => x.Password == company.Password);
                return View();
            }
        }
        [HttpGet]
        public ActionResult SelectCompany()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult SelectCompany(CompanyViewModel model)
        {
                var user = User.Identity.GetUserId();
        
            var Company = db.Companies.FirstOrDefault(x => x.Password == model.Password && x.CompanyName == model.CompanyName);
            var PartOfCompany = db.CompanyEmployees.Any(x => x.EmployeeId == user);
            ViewBag.employee= db.CompanyEmployees.Any(x => x.EmployeeId == user);
            var employee = db.Employees.SingleOrDefault(x => x.Id == user);
           // RegisterViewModel names = TempData["Names"] as RegisterViewModel;
            if (db.Companies.Any(x => x.CompanyName == model.CompanyName && x.Password == model.Password &&PartOfCompany==false))
            {
                db.CompanyEmployees.Add(new CompanyEmployee { CompanyId = Company.CompanyId, EmployeeId = user,Name=employee.FirstName+employee.LastName });
               db.IdentityUserRoles.Add(new IdentityUserRole { UserId = user, RoleId = Employee });
                db.SaveChanges();
                return RedirectToAction("EmployeeDashboard", "WorkSchedules");
            }
            else
            {           
                return View();
            }
        } 
        [Authorize(Roles = "Ceo,Senior Managment,Human resources department")]
        public ActionResult Details()
        {        
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);
            var Company = db.Companies.Find(company.CompanyId);
            return View(Company);
        }
        [Authorize(Roles ="Ceo")]
         [HttpGet]
   public ActionResult Edit()
        {
            CompanyViewModel model = new CompanyViewModel();
            var intervals = model.Interval();
            ViewBag.ClientBillInterval = new SelectList(intervals);
            ViewBag.EmployeePayInterval = new SelectList(intervals);
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);
            var Company = db.Companies.Find(company.CompanyId);
            return View(Company);
        }
        [HttpPost]
        [Authorize(Roles = "Ceo")]
        public ActionResult Edit(Company company)
        {
            var Company = db.Companies.Find(company.CompanyId);
            Company.Address = company.Address;
            Company.ClientBillInterval = company.ClientBillInterval;
            Company.CompanyName = company.CompanyName;
            Company.CompanyPhoneNumber = company.CompanyPhoneNumber;
            Company.Email = company.Email;
            Company.EmployeePayInterval = company.EmployeePayInterval;
            Company.Password = company.Password;
            db.Entry(Company).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details");
        }
        [Authorize(Roles = "Ceo")]
        [HttpGet]
        public ActionResult Delete()
        {
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);
            var Company = db.Companies.SingleOrDefault(x => x.CompanyId == company.CompanyId);
        
            return View(Company);
        }
        [HttpPost]
        [Authorize(Roles = "Ceo")]
        public ActionResult Delete(Company orginaztion)
        {
            try
            {
                var user = User.Identity.GetUserId();
                var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);
                var Company = db.Companies.SingleOrDefault(x => x.CompanyId == company.CompanyId);
                if (Company.Password == orginaztion.Password && Company.Email == orginaztion.Email)
                {
                    var AmountOfEmployees = db.CompanyEmployees.Include("Employee").Where(x => x.CompanyId == Company.CompanyId).Count();
                    var Employees = db.CompanyEmployees.Include("Employee").Where(x => x.CompanyId == Company.CompanyId);
                    foreach (var item in Employees)
                    {
                        var Identity = db.Employees.Find(item.EmployeeId);
                        db.CompanyEmployees.Remove(item);
                        db.Employees.Remove(Identity);
                    }


                    db.Companies.Remove(Company);
                    db.DeletedCompanies.Add(new DeletedCompany { CompanyName = Company.CompanyName, AmountOfEmployees = AmountOfEmployees, DateJoined = Company.DateJoined, DateLeft = DateTime.Today });
                    db.SaveChanges();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("delete", "The delete didn't work.Check that you have the correct email and password and try again.");
                    return View();
                }
            }
            catch
            {
                return View(orginaztion);
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
