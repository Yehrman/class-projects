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
using CompuskillsMvcProject.Models;

namespace CompuskillsMvcProject.Controllers
{
    [Authorize(Roles ="Ceo, Senior Managment,Finance Department")]
    public class ClientBillsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();

        // GET: ClientBills
        public ActionResult Index( DateTime? from=null,DateTime? to=null, string name = "")
        {
            from = from ?? DateTime.Now.AddMonths(-1);
            to = to ?? DateTime.Now;
            var user = User.Identity.GetUserId();
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == user);
            var clientBills = db.ClientBills.Include(c => c.Client).Include(c => c.Company).Where(x => x.CompanyId == company.CompanyId&& x.Client.ClientName.Contains(name) && x.DateBilled >= from && x.DateBilled <= to).OrderByDescending(x => x.DateBilled);
                return View(clientBills);              
            }
        //manual BillClients needs work
  

        public ActionResult Bill()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Search()
        {
            return PartialView();
        }
  [HttpPost]
        public ActionResult Search(DateTime? from,DateTime? to,string name="")
        {
            return RedirectToAction("Index", new { from, to, name });
        }
        // GET: ClientBills/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.error = id == null;
     
            ClientBill clientBill = db.ClientBills.Find(id);
            ViewBag.ClientError = clientBill == null;
            return View(clientBill);
        }
        Payment payment = new Payment(); 
         Dictionary<int, TimeSpan> TimeWorked(string time,int projectId=0)
        {
            Dictionary<int, TimeSpan> timeWorked = new Dictionary<int, TimeSpan>();
            var user = User.Identity.GetUserId();
            if (time == "On a per project basis")
            {
                timeWorked = payment.BillProjectEntries(projectId, user);
            }
            else
            {
                timeWorked = payment.EntriesForClients(user, time);
            }
            return timeWorked;
        }

        public ActionResult BillClients(string time)
        {            
            var Total = TimeWorked(time);
   
            foreach (var item in Total)
            {
                var s = Convert.ToString(item.Value);
                var Hours = Convert.ToDecimal(TimeSpan.Parse(s).Hours);
                var Minutes = Convert.ToDecimal(TimeSpan.Parse(s).Minutes);
                var Seconds = Convert.ToDecimal(TimeSpan.Parse(s).Seconds);
                var findUser = User.Identity.GetUserId();
                var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == findUser);
                var rate = db.ClientProjects.Include("Client").FirstOrDefault(x => x.Client.CompanyId == company.CompanyId && x.ProjectId == item.Key);
                var payPerHour = rate.BillRate * Hours;
                var perMinute = rate.BillRate / 60;
                var payPerMinute = perMinute * Minutes;
                var perSecond = rate.BillRate / 3600;
                var payPerSecond = perSecond * Seconds;
                var Bill = payPerHour + payPerMinute + payPerSecond;
                var TotalBill = Bill.GetValueOrDefault();
                if (TotalBill > 0)
                {
                    db.ClientBills.Add(new ClientBill { CompanyId = company.CompanyId, ClientId = rate.ClientId, Bill = TotalBill, DateBilled = DateTime.Today,TimeWorked=item.Value,ProjectId=rate.ProjectId });
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

  
        public ActionResult PaymentRecieved( int id)
        {
            var clientBill = db.ClientBills.Find(id);
            clientBill.DatePaymentRecieved = DateTime.Today;
                db.Entry(clientBill).State = EntityState.Modified;
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
