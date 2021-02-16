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
{[Authorize(Roles = "Ceo,Senior Managment,Human resources department")]
    public class ClientsController : Controller
    {
        private TimeSheetDbContext db = new TimeSheetDbContext();    
        public ActionResult Index(string name="")
        {
            var UserId = User.Identity.GetUserId();
            var Company = db.CompanyEmployees.FirstOrDefault(x => x.EmployeeId == UserId);
            var Clients = db.Clients.Where(x => x.CompanyId == Company.CompanyId && x.IsDeleted == false && x.ClientName.Contains(name));
            return View(Clients);
            }
   
        // GET: Clients/Details/5
        [HttpGet]
        public ActionResult Search()
        {
            return PartialView ();
        }
        [HttpPost]
        public ActionResult Search(string name)
        {
            return RedirectToAction("Index",new { name });
        }
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
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == UserId);
            if (ModelState.IsValid)
            {
                //errors 1) both bools unselected 2)both selected 3)billByProject selected and bill rate is not null 4)billByClient is true and bill rate is null 
                if (db.Clients.Any(x => x.ClientEmail == client.ClientEmail &&x.CompanyId==company.CompanyId))
                {//Make this partial view
                    return RedirectToAction("EmailError");
                }
              else if(client.BillByProject==false && client.BillByClient==false)
                {
                    ModelState.AddModelError("bools", "You must select either to bill by client or project");
                    return View();
                }
                else if(client.BillByClient==true && client.BillByProject==true)
                {
                    return View();
                }
                else if(client.BillByProject==true && client.BillRate!=null)
                {     
                    return View();
                }
                else if(client.BillByClient==true && client.BillRate==null)
                {
                    return View();
                }
                //If email is different but everthing else is the same add new row in client
                else
                {
                    if(client.BillByClient==true)
                    {
                        client.BillByProject = false;
                    }
                    else if(client.BillByProject==true)
                    {
                        client.BillByClient = false;
                        client.BillRate = null;
                    }
                    db.Clients.Add(new Client { CompanyId=company.CompanyId, ClientName = client.ClientName, ClientEmail=client.ClientEmail,ClientPhoneNumber=client.ClientPhoneNumber,ClientAddress = client.ClientAddress,BillByClient=client.BillByClient,BillRate=client.BillRate,BillByProject=client.BillByProject ,IsDeleted=false});
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(client);
        }
            [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.error = id == null;
            var client = db.Clients.Find(id);
            ViewBag.clientError = client == null;
            return View(client);
        }
        [HttpPost]
        //debug edit
        public ActionResult Edit (Client client,int id)
        {
            var UserId = User.Identity.GetUserId();
            var company = db.CompanyEmployees.SingleOrDefault(x => x.EmployeeId == UserId);
            var editedClient = db.Clients.Find(id);
          if(client.ClientEmail!=editedClient.ClientEmail&&db.Clients.Any(x=>x.ClientEmail==client.ClientEmail))
            {
                return RedirectToAction("EmailError");
            }
       
            else if (client.BillByProject == false && client.BillByClient == false)
            {
                ModelState.AddModelError("bools", "You must select either to bill by client or project");
                return View();
            }
            else if (client.BillByClient == true && client.BillByProject == true)
            {
                return View();
            }
            else if (client.BillByProject == true && client.BillRate != null)
            {
                return View();
            }
            else if (client.BillByClient == true && client.BillRate == null)
            {
                return View();
            }
            //If email is different but everthing else is the same add new row in client
            else
            {
                editedClient.ClientName = client.ClientName;
                editedClient.ClientPhoneNumber = client.ClientPhoneNumber;
              
                editedClient.ClientEmail = client.ClientEmail;
                editedClient.ClientAddress = client.ClientAddress;
                if (client.BillByClient == true)
                {
                    client.BillByProject = false;
                }
                else if (client.BillByProject == true)
                {
                    client.BillByClient = false;
                    client.BillRate = null;
                }
                editedClient.BillByClient = client.BillByClient;
                editedClient.BillByProject = client.BillByProject;
                editedClient.BillRate = client.BillRate;
                db.Entry(editedClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

     //make edit post with client.cs

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
                ViewBag.error = id == null;
            Client client = db.Clients.Find(id);
            ViewBag.clientError = client == null; 
            return View(client);
        }

        // POST: Clients/Delete/5
    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
         
            //var currentUser = User.Identity.GetUserId();
            //var Company = db.CompanyEmployees.FirstOrDefault(x => x.EmployeeId == currentUser);
         
                    Client client = db.Clients.Find(id);
                
                   client.IsDeleted = true;
                    db.Entry(client).State = EntityState.Modified;           
                    db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ViewDeletedClients()
        {
            var currentUser = User.Identity.GetUserId();
            var Company = db.CompanyEmployees.FirstOrDefault(x => x.EmployeeId == currentUser);
            var DeletedClients = db.Clients.Where(x => x.CompanyId==Company.CompanyId && x.IsDeleted==true);
            return View(DeletedClients);        
        }
        public ActionResult UndoDelete(int id)
        {
            //var currentUser = User.Identity.GetUserId();
          
                var deletedClient = db.Clients.Find(id);

                deletedClient.IsDeleted = false;
                db.Entry(deletedClient).State = EntityState.Modified;
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }     
        public ActionResult EmailError()
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
