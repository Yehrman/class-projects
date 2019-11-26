using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CompuskillsDatabaseProject
{
    class Program
    {
        static void Main(string[] args)
        {
           SecurityRepository securityRepository = new SecurityRepository();
            //    var begin = DateTime.Parse("2019, 06, 12");
            //  var end = DateTime.Parse("2019,09,16");

            // securityRepository.GrantAccess("Ceo's suite");
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                //  string a = Console.ReadLine();
                // int b = Convert.ToInt32(a);
                var credentials = from credential in securityContext.EmployeeCredentials.Include("EmployeeSecurityDevices")
                                  where credential.EmployeeSecurityDevices.Any(x=>x.EmployeeId==3)
                                  select credential;
                securityRepository.IsAuthorized(1,credentials ,3);
                securityRepository.IsAuthorized(3, credentials, 3);
            }
             securityRepository.AddSecurityDevice("Face Recognition");

            Console.ReadKey();
         
                }
          }
        }
   
