using GeoCode.GeoCoding_Art;
using Newtonsoft.Json; //added JSON.NET with Nuget
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
namespace GeoCode
{
    class Program
    {

        const string Api = "AIzaSyCRQ2A5WO3oLqDrjyQhx6BRmf5KSgoo950";
        static string url = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        static string url2= "&key=" + Api+"&sensor=false";
        static void Main(string[] args)
        {
            using (FittnesAppContext fitt = new FittnesAppContext())
            {
                void GeoCoding(string Address)
                {



                    var Result = new WebClient().DownloadString(url + Address + url2);
                    Response jsonResult = JsonConvert.DeserializeObject<Response>(Result);
                    string status = jsonResult.status;
                    string lad = string.Empty;
                    string lon = string.Empty;
                    if (status == "OK")
                    {
                        for (int i = 0; i < jsonResult.results.Length; i++)
                        {
                            lad += "Latitude" + jsonResult.results[i].geometry.location.lat;
                              lon+=   "/Longitude" + jsonResult.results[i].geometry.location.lng;

                        }

                        fitt.NoGoZones.Add(new NoGoZone() { Addresses=Address, Laditude = lad, Longitude = lon });
                    }
                    else

                       Console.WriteLine(status);
                }
                foreach (var item in fitt.UsersNoZones)
                {
                    Console.WriteLine(item.Users.Name);
                    Console.WriteLine(item.NoGoZones.Addresses);
                }
             /*   void AddUser (string name)
                {
                    app.Users.Add(new User { Name = name });
                }

                Console.WriteLine("Type in your name");
                string I = Console.ReadLine();
                AddUser(I);
                app.SaveChanges();
                int j= 0;
                while(j<3)
                {
                    Console.WriteLine("Enter a address");
                    string input = Console.ReadLine();
                    GeoCoding(input);
                   
                    j++;
                }*/
              //  app.SaveChanges();
            }
               
           
        
        }
          
        }
           
           
    }

