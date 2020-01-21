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
{     [Authorize]
    public class ClientsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }
        public ActionResult UserIndex()
        {
            var UserId = User.Identity.GetUserId();
            var Clients = db.UserClients.Include("Client").Where(x => x.TtpUserId == UserId);
            return View(Clients);
        }
        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            
            ViewBag.error = id == null;
            Client client = db.Clients.FirstOrDefault(x =>  x.ClientId == id);
            ViewBag.clientError = client == null;
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CreateClientModel client)
        {
            var UserId = User.Identity.GetUserId();
            
            if (ModelState.IsValid)
            {
                if (db.Clients.Any(x => x.ClientEmail == client.ClientEmail&&x.ClientName==client.ClientName))
                {
                    var Customer = db.Clients.FirstOrDefault(x => x.ClientEmail == client.ClientEmail && x.ClientName == client.ClientName);
                    var id = Customer.ClientId;
                    db.UserClients.Add(new UserClient {  TtpUserId = UserId,ClientId=id });                 
                    db.SaveChanges();
                    return RedirectToAction("UserIndex");
                }
               else if(db.Clients.Any(x=>x.ClientEmail==client.ClientEmail&&x.ClientName!=client.ClientName))
                {
                    ModelState.AddModelError("ClientEmail", "That email is taken please add a unique email");
                }
                else
                {
                    db.Clients.Add(new Client { ClientName=client.ClientName,ClientEmail=client.ClientEmail});
                    db.SaveChanges();
                    var Customer = db.Clients.FirstOrDefault(x => x.ClientEmail == client.ClientEmail && x.ClientName == client.ClientName);
                    var id = Customer.ClientId;
                    db.UserClients.Add(new UserClient { TtpUserId = UserId,ClientId=id });
                    db.SaveChanges();
                    return RedirectToAction("UserIndex");
                }
            }

            return View(client);
        }
//Can't edit since the clients table is all users clients 1 user can't edit without the other
        public ActionResult Edit()
        {           
            return View();
        }

     

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
                ViewBag.error = id == null;
            Client client = db.Clients.Find(id);
            ViewBag.clientError = client == null; 
            return View(client);
        }

        // POST: Clients/Delete/5
        //make sure to 1st check userclients if there is thos client more then once
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var currentUser = User.Identity.GetUserId();
            var userClient = db.UserClients.FirstOrDefault(x => x.TtpUserId == currentUser && x.ClientId == id);
            db.UserClients.Remove(userClient);
      
            var multipleClient = db.UserClients.Where(x => x.ClientId == id).Count();
            if(multipleClient<=1)
            {
                Client client = db.Clients.Find(id);
                db.Clients.Remove(client);
            }     
            db.SaveChanges();

            return RedirectToAction("UserIndex");
        }
        //Must calculate parts of hours and count hours and pay accordingly
        public ActionResult BillTotal(int id)
        {
         
            var findUser = User.Identity.GetUserId();
            var Project = db.Projects.Find(id);
            var Rate = Project.BillRate;
            var ProjectId = Project.ProjectId;
            var Entries = db.TimeSheetEntries.Where(x => x.TtpUserId == findUser && x.ProjectId == ProjectId);
            TimeSpan? Hours;
            TimeSpan TotalTimeWorked = new TimeSpan();
            decimal TotalHours=0;
            decimal Minutes = 0;
            TimeSpan nonNull = new TimeSpan();
            foreach (var item in Entries)
            {            
               Hours = item.EndTime - item.StartTime;
                if(Hours.HasValue)
                {
                    nonNull = Hours.Value;
                     Hours=nonNull;
                }
                TotalTimeWorked += nonNull;
                string s = Convert.ToString(TotalTimeWorked);
                decimal Total = Convert.ToDecimal(TimeSpan.Parse(s).Hours);
                decimal minutes = Convert.ToDecimal(TimeSpan.Parse(s).Minutes);
                  TotalHours += Total;
                Minutes += minutes;
            }
            
            string TotalTime = string.Format(" {0} hours, {1} minutes, {2} seconds",
TotalTimeWorked.Hours, TotalTimeWorked.Minutes, TotalTimeWorked.Seconds);
            var PayHours = TotalHours * Project.BillRate;
             var PayForMinutes = Project.BillRate / 60;
            var PayMinutes = Minutes * PayForMinutes;
            var Pay = PayHours + PayMinutes;
            var bill = Project.TotalBill = Pay;
          //  var hours = Project.TotalHours = TotalHours;
            db.Entry(Project).CurrentValues.SetValues(bill);
          //  db.Entry(Project).CurrentValues.SetValues(hours);
            db.SaveChanges();
            ViewBag.Hours = TotalTime;
            ViewBag.Client = Project.Client.ClientName;
            ViewBag.Project = Project.ProjectName;
            ViewBag.Total = Project.TotalBill;
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
