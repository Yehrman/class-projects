using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuskillsDatabaseProject
{

    class SecurityRepository
    {
        public void AddSecurityDevice(string securityDevice)
        {
            using (BuildingSecurityContext SecurityContext = new BuildingSecurityContext())
            {
                if (SecurityContext.SecurityDevices.All(x => x.SecurityDeviceType != securityDevice))
                {
                    SecurityContext.SecurityDevices.Add(new SecurityDevice { SecurityDeviceType = securityDevice });
                    SecurityContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("This device already is installed in our system");
                }            
            }
        }
        public IQueryable<AccessHistory> GetActivity(DateTime begin, DateTime to)
        {
     
            using (BuildingSecurityContext SecurityContext = new BuildingSecurityContext())
            {
                var dates = from total in SecurityContext.AccessHistories.Include("Door").Include("Employees")
                            where total.AttemptDate >= begin && total.AttemptDate <= to
                            select total;
                Console.WriteLine("To print the results type the word print");
                string print = Console.ReadLine();
                if (print == "print")
                {
                    foreach (var item in dates)
                    {
                        Console.WriteLine(item.AccessHistoryID);
                        Console.WriteLine(item.AttemptDate);
                        Console.WriteLine(item.Door.Room);
                        Console.WriteLine(item.Employees.Name);
                        Console.WriteLine(item.Result);
                    }
                }
                return dates;
            }
        }
        public IQueryable<AccessHistory> GetDoorActivity(DateTime begin, DateTime to,string door)
        {
       
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
               var id= GetDoorId(door);       
                var dates = from total in securityContext.AccessHistories.Include("Door").Include("Employees")
                            where total.AttemptDate >= begin && total.AttemptDate <= to
                            where total.DoorID == id
                select total;
                Console.WriteLine("To print the results type the word print");
                string print = Console.ReadLine();
                if (print == "print")
                {
                    foreach (var item in dates)
                    {
                        Console.WriteLine(item.AccessHistoryID);
                        Console.WriteLine(item.AttemptDate);
                        Console.WriteLine(item.Door.Room);
                        Console.WriteLine(item.Employees.Name);
                        Console.WriteLine(item.Result);
                    }
                }
                return dates;
            }

        }
        public IQueryable<AccessHistory> GetSuspiciousActivity(DateTime begin, DateTime to)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {

                IQueryable<AccessHistory> result = Enumerable.Empty<AccessHistory>().AsQueryable();
                var dates = from total in securityContext.AccessHistories
                            where total.AttemptDate >= begin && total.AttemptDate <= to
                            where total.Result == false
                            select total;
             //   var temp = securityContext.AccessHistories.Where(x => x.AttemptDate >= begin && x.AttemptDate <= to && x.Result == false);
                foreach (var item in dates.ToList())
                {
                    var res = item.AttemptDate.AddMinutes(2);
                    result = dates.Where(x => x.AttemptDate == item.AttemptDate || x.AttemptDate > res && x.Result != true);
                }
                Console.WriteLine("To print the results type the word print");
                string print = Console.ReadLine();
                if (print == "print")
                {
                    foreach (var item in result.ToList())
                    {
                        Console.WriteLine(item.AccessHistoryID);
                        Console.WriteLine(item.AttemptDate);
                        Console.WriteLine(item.Door.Room + "\t" + item.DoorID);
                        Console.WriteLine(item.Employees.Name + "\t" + item.EmployeeId);
                        Console.WriteLine(item.Result);
                    }
                }

                return result;
                
            }
               
            }
                  
        public void GrantAccess(string door, IQueryable<DoorSecurityDevice> credentials)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {            
                Console.WriteLine("These are the credentials currently with this door");
                Console.WriteLine("Credential type \t CredentialId");
                foreach (var item in credentials)
                {
                    Console.WriteLine(item.SecurityDevices.SecurityDeviceType + "\t" + item.SecurityDeviceId);
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("Type which of these credentials would you like to add");
                Console.WriteLine("Credential type \t CredentialId");
                foreach (var item in securityContext.SecurityDevices)
                {
                    Console.WriteLine(item.SecurityDeviceType + "\t" + item.SecurityDeviceId);
                    Console.WriteLine();
                }
                string credential = Console.ReadLine();
        

                if (securityContext.SecurityDevices.Any(x => x.SecurityDeviceType == credential))
                {
                    var ID = GetSecurityDeviceId(credential);
                    if (credentials.Any(x => x.SecurityDevices.SecurityDeviceType == credential))
                    {
                        Console.WriteLine("The door already has this credential");
                    }                  
                  else  if (credentials.All(x => x.SecurityDeviceId != ID))
                    {
                        var doorId = GetDoorId(door);
                        securityContext.DoorSecurityDevices.Add(new DoorSecurityDevice { DoorId = doorId, SecurityDeviceId = ID });
                        securityContext.SaveChanges();
                    }
                }
              else 
                {
                    Console.WriteLine("No such credential exists");
                }
            }
        }
        
        public bool IsAuthorized(string name, IEnumerable<EmployeeCredential> credentials, string door)
        {
            
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                var id = GetEmployeeId(name);
                var doorid = GetDoorId(door);
                bool Authorized = false;           
                var DoorAccess = from access in securityContext.DoorSecurityDevices
                                 where access.DoorId == doorid
                                 orderby access.SecurityDeviceId
                                 select access.SecurityDeviceId;
                foreach (var item in DoorAccess)
                {
                    if (credentials.Any(x => x.EmployeeSecurityDevices.Any(y => y.SecurityDeviceId == item)))
                    {
                        Authorized = true;
                    }
                    else if (credentials.Any(x => x.EmployeeSecurityDevices.Any(y => y.SecurityDeviceId != item)))
                    {
                        Authorized = false;
                        break;
                    }
                }
                LogAuthorizationAttempt(doorid, Authorized, id);
                return Authorized;

            }
        }
        public void LogAuthorizationAttempt(int doorId, bool result, int id)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                securityContext.AccessHistories.Add(new AccessHistory { DoorID = doorId, AttemptDate = DateTime.Now, EmployeeId = id, Result = result });
                securityContext.SaveChanges();
            }
        }
        public void RevokeAccess()
        {
            Console.WriteLine("Please select a door  to remove a credential from this list");
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                foreach (var item in securityContext.Doors)
                {
                    Console.WriteLine(item.Room);
                }
                string door = Console.ReadLine();
                if (securityContext.Doors.Any(x => x.Room == door))
                {
                    var doorId = GetDoorId(door);
                    var doorCreds = securityContext.DoorSecurityDevices.Include("SecurityDevices").Where(x => x.DoorId == doorId);
                    Console.WriteLine("Please select a credential to remove from this list");
                    foreach (var item in doorCreds)
                    {
                        Console.WriteLine(item.SecurityDevices.SecurityDeviceType+"\t"+item.SecurityDeviceId);
                    }
                    string credential = Console.ReadLine();
                    if(doorCreds.Any(x=>x.SecurityDevices.SecurityDeviceType==credential))
                    {
                      var deviceId=  GetSecurityDeviceId(credential);
                        if(doorCreds.Any(x=>x.SecurityDeviceId>deviceId))
                        {
                            Console.WriteLine("Please delete a the device with the highest deviceId 1st ");
                        }
                        else
                        {
                            var credToRemove = securityContext.DoorSecurityDevices.FirstOrDefault(x => x.SecurityDeviceId == deviceId && x.DoorId == doorId);
                            securityContext.DoorSecurityDevices.Remove(credToRemove);
                            securityContext.SaveChanges();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please select a credential to remove from the above list");
                    }
                }
                else
                {
                    Console.WriteLine("Please select a door from the above list");
                }
                 
                 
                }
                }
            
        
       internal int GetDoorId(string door)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {

                var entrance = securityContext.Doors.SingleOrDefault(x => x.Room == door);
                var id = entrance.DoorId;
                return id;
            }
        }
        private int GetEmployeeId(string employee)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                var worker = securityContext.Employees.SingleOrDefault(x => x.Name == employee);
                var id = worker.EmployeeId;
                return id;
            }
        }
        internal int GetSecurityDeviceId(string credential)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                var device = securityContext.SecurityDevices.SingleOrDefault(x => x.SecurityDeviceType == credential);
                var id = device.SecurityDeviceId;
                return id;
            }
        }
    }
}
        
    



