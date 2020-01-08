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
{
    public class ClientsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: Clients
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
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

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,ClientName,ClientEmail")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Bill()
        {
            var findUser = User.Identity.GetUserId();
            // ViewBag.ProjectId = new SelectList(db.Projects.Where(x => x.TtpUserId == currentUser), "ProjectId", "ProjectName");
            ViewBag.Client = new SelectList(db.Projects.Include("Client").Where(x => x.TtpUserId == findUser), "Client.Name", "ClientId");
            // ViewBag.Project = new SelectList(db.Projects.Where(x => x.TtpUserId == findUser), "ProjectName", "ProjectName");
            return View();
        }
        [HttpPost]
        public ActionResult BillTotal(string Client, string project)
        {
            var findUser = User.Identity.GetUserId();
            var Project = db.Projects.Include("Client").FirstOrDefault(x => x.TtpUserId == findUser && x.Client.ClientName == Client && x.ProjectName == project);
            var Rate = Project.BillRate;
            var ProjectId = Project.ProjectId;
            ViewBag.Client = Project.Client.ClientName;
            var Entries = db.TimeSheetEntries.Where(x => x.TtpUserId == findUser && x.ProjectId == ProjectId);
            TimeSpan? Hours;
            decimal TotalHours = 0;
            decimal Total = 0;
            foreach (var item in Entries)
            {
                Hours = item.EndTime - item.StartTime;
                Total = Convert.ToDecimal(Hours);
                TotalHours += TotalHours;
            }
            var Payment = TotalHours * Rate;
            ViewBag.Total = Payment;
            return PartialView(Payment);

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
