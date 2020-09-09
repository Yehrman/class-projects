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
                DateTime TimeFrom(string from)
            {        
                var since = DateTime.Parse(from);
                return since;
            }
            DateTime TimeTo(string to)
            {
                var untill = DateTime.Parse(to);
                return untill;
            }
            Console.WriteLine("Welcome to the administrative system of building security please type in what you would like to do");
            Console.WriteLine();
            SecurityRepository securityRepository = new SecurityRepository();
            while(true)
            {
                Console.WriteLine("Please type a to add a device\n or type g to get activity\n or type gd to get 1 doors activity\n or type gs to get suspicious activity \n" +
                        "\n type ga to grant a door auth credentials\n or auth to gain entry based on your credentials \n or rev to take a credential from a door ");

                using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
                {
                     string option = Console.ReadLine();
                 
                    if (option== "a")
                {
                        Console.WriteLine("Please type the name of the device");
                        string param = Console.ReadLine();
                        securityRepository.AddSecurityDevice(param);
                }
                else if(option=="g")
                {
                        Console.WriteLine("Please type a date from and a date to in mth-dd-year format");
                        string from = Console.ReadLine();
                        string till = Console.ReadLine();
                        securityRepository.GetActivity(TimeFrom(from),TimeTo(till));
                }
                else if(option=="gd")
                {
                        Console.WriteLine("Please type a date from and a date to in mth-dd-year format and a door name");
                        string from = Console.ReadLine();                     
                        string till = Console.ReadLine();
                        string param = Console.ReadLine();
                        securityRepository.GetDoorActivity(TimeFrom(from),TimeTo(till), param);
                }
                else if(option=="gs")
                {
                        Console.WriteLine("Please type a date from and a date to in mth-dd-year format");
                        string from = Console.ReadLine();
                        string till = Console.ReadLine();
                      var result= securityRepository.GetSuspiciousActivity(TimeFrom(from),TimeTo(till));
                  
                    }
                else if(option=="ga")
                {
                        Console.WriteLine("Please type the door name to add credentials to");
                        string param = Console.ReadLine();
                        try
                        {
                            var id = securityRepository.GetDoorId(param);
                            var DeviceList = from credential in securityContext.DoorSecurityDevices.Include("SecurityDevices")
                                             where credential.DoorId == id
                                             select credential;

                            securityRepository.GrantAccess(param, DeviceList);
                        }
                        catch
                        {
                            Console.WriteLine("No such door exists in the facility");
                        }
                }
                    else if(option=="auth")
                    {
                       //type name
                        string param = Console.ReadLine();
                        var Empcredentials = from credential in securityContext.EmployeeCredentials.Include("EmployeeSecurityDevices").Include("Employee")
                                             where credential.EmployeeSecurityDevices.Any(x => x.Employee.Name == param)
                                             select credential;     
                       //type door
                        string param2 = Console.ReadLine();
                        securityRepository.IsAuthorized(param, Empcredentials, param2);
                    }
                   
                    else if (option=="rev")
                    {                  
                            securityRepository.RevokeAccess();
                  }
          
                }
            }
                }
          }
        }
   
