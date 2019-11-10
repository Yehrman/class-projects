using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using Newtonsoft.Json; //added JSON.NET with Nuget
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        static List<Response> GetLocations = new List<Response>() { };
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
       
        public ActionResult LadLon()
        {
            return View();
            }
        
        public ActionResult GeoCode(Places places)
        {
            const string Api = "Api key ";
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + places + "&key=" + Api + "&sensor=false";

            Response jsonResult = JsonConvert.DeserializeObject<Response>(Result);
            string location = string.Empty;
            string status = jsonResult.status;
            if (status == "OK")
            {
                for (int i = 0; i < jsonResult.results.Length; i++)
                {
                    location += "Latitude" + jsonResult.results[i].geometry.location.lat +
                         "/Longitude" + jsonResult.results[i].geometry.location.lng;
                }
                return View(location);
            }
            else
            {
                return View(status);
            }

        }


    }
           
        }

     

