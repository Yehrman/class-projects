using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using FittnessApp.Models;
using Newtonsoft.Json; //added JSON.NET with Nuget
using Newtonsoft.Json.Linq;
using System.Net;

namespace FittnessApp.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult InputLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void GetLadLon(string  adressess )
        {
        const string Api = "Api key";
        string url = "https://maps.googleapis.com/maps/api/geocode/json?address=";
         string url2 = "&key=" + Api + "&sensor=false";
            using (FittApp fitt = new FittApp())
            {
                var Result = new WebClient().DownloadString(url + adressess + url2);
                Response jsonResult = JsonConvert.DeserializeObject<Response>(Result);
                string status = jsonResult.status;
                string lad = string.Empty;
                string lon = string.Empty;
                if (status == "OK")
                {
                    for (int i = 0; i < jsonResult.results.Length; i++)
                    {
                        lad += "Latitude" + jsonResult.results[i].geometry.location.lat;
                         lon+= "/Longitude" + jsonResult.results[i].geometry.location.lng;

                    }
                    fitt.NoGos.Add(new NoGoZones() { id = 1, Address = adressess, Laditude = lad, Longitude = lon });
                    fitt.SaveChanges();
                    Redirect("Location/Index");
                }
                       
  else
                {
                    Redirect("Home/Index");
                }
             
            }
        }
      
}
    }
