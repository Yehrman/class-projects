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
            using (BuildingSecurityContext building = new BuildingSecurityContext())
            {
                building.SecurityDevices.Add(new SecurityDevice { SecurityDeviceType = securityDevice });
                building.SaveChanges();
            }
        }
    public IQueryable<AccessHistory> GetActivity(DateTime begin, DateTime to)
    {
       using (BuildingSecurityContext SecurityContext = new BuildingSecurityContext())
            { 

            var dates = from total in SecurityContext.AccessHistories.Include("Door").Include("Employees")
                        where total.AttemptDate >= begin && total.AttemptDate <= to
                        select total;
                foreach (var item in dates)
                {
                    Console.WriteLine(item.AccessHistoryID);
                    Console.WriteLine(item.AttemptDate);
                    Console.WriteLine(item.Door.Room);
                    Console.WriteLine(item.Employees.Name);
                    Console.WriteLine(item.Result);
                }
                return dates;
        }    
        }
        public IQueryable<AccessHistory> GetDoorActivity(DateTime begin, DateTime to, int doorId)
        {
            using (BuildingSecurityContext securityContext= new BuildingSecurityContext())
            {
                var door = securityContext.AccessHistories.Find(doorId);
                var dates = from total in securityContext.AccessHistories.Include("Door").Include("Employees")
                            where total.AttemptDate >= begin && total.AttemptDate <= to
                            where total.DoorID==doorId
                            select total;
                foreach (var item in dates)
                {
                    Console.WriteLine(item.AccessHistoryID);
                    Console.WriteLine(item.AttemptDate);
                    Console.WriteLine(item.Door.Room);
                    Console.WriteLine(item.Employees.Name);
                    Console.WriteLine(item.Result);
                }
                return dates;
            }
           
        }
        public IQueryable<AccessHistory> GetSuspiciousActivity(DateTime begin, DateTime to)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                var dates = from total in securityContext.AccessHistories.Include("Door").Include("Employees")
                            where total.AttemptDate >= begin && total.AttemptDate <= to
                            where total.Result == false
                            select total;
                //  bool result = true;


                foreach (var item in dates)
                {
                    var temp = item.AttemptDate.AddMinutes(2);

                    var tem = securityContext.AccessHistories.Include("Door").Include("Employees").Where(x => x.AttemptDate >= begin && x.AttemptDate <= to && x.Result == false);
                    if (tem.Any(x => x.AttemptDate == temp))
                    {
                        tem = dates;
                    }
                }
                foreach (var item in dates)
                {
                    Console.WriteLine(item.AccessHistoryID);
                    Console.WriteLine(item.AttemptDate);
                    Console.WriteLine(item.Door.Room);
                    Console.WriteLine(item.Employees.Name);
                    Console.WriteLine(item.Result);
                }
                return dates;
            }
        }

        public void GrantAccess(int doorId, IEnumerable<EmployeeSecurityDevice> credentials)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                bool Authorized = false;
                var DoorAccess = from access in securityContext.DoorSecurityDevices.Include("SecurityDevices ")
                                 where access.DoorId == doorId
                                 orderby access.SecurityDeviceId
                                 select access.SecurityDevices.SecurityDeviceType;
                // string a = DoorAccess.ToString();
                foreach (var item in DoorAccess)
                {
                    if (credentials.Any(x => x.SecurityDevices.SecurityDeviceType == item))
                    {
                        Authorized = true;
                    }
                    else if (credentials.Any(x => x.SecurityDevices.SecurityDeviceType != item))
                    {
                        Authorized = false;
                        break;
                    }
                }
            }
        }
        public bool IsAuthorized(int doorId, IEnumerable<EmployeeCredential> credentials, int empId)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                bool Authorized = false;
                var DoorAccess = from access in securityContext.DoorSecurityDevices
                                 where access.DoorId == doorId
                                 orderby access.SecurityDeviceId
                                 select access.SecurityDeviceId;
                foreach (var item in DoorAccess)
                {
                    if (credentials.Any(x => x.EmployeeSecurityDevices.Any(y=>y.SecurityDeviceId==item)))
                    {
                        Authorized = true;
                    }
                    else if (credentials.Any(x => x.EmployeeSecurityDevices.Any(y => y.SecurityDeviceId != item)))
                    {
                        Authorized = false;
                        break;
                    }
                }            
                    LogAuthorizationAttempt(doorId, Authorized, empId);
                    return Authorized;
                
            }
        }
        public void LogAuthorizationAttempt(int doorId, bool result,int id=1)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                securityContext.AccessHistories.Add(new AccessHistory {  DoorID = doorId,AttemptDate=DateTime.Now,EmployeeId=id, Result = result });
                securityContext.SaveChanges();
            }
        }


     /*   public void SetEmployeeSecurityId(int PersonId)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
                int ranking = PersonId;
                var devices = from device in securityContext.EmployeeSecurityDevices
                              where device.EmployeeId == PersonId
                              select device.SecurityDeviceId;
                foreach (var item in devices)
                {
                    ranking += item;
                }
                securityContext.EmployeeAccessRights.Add(new EmployeeAccessRight { EmployeeId = PersonId, EmployeeSecurityLevelRanking = ranking });
                securityContext.SaveChanges();
            }
        }*/
public void Add (int id,byte bt=0)
        {
            using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
            {
               var guid= Guid.NewGuid();
                var a = securityContext.Employees.SingleOrDefault(x => x.EmployeeId == id);
                securityContext.EmployeeCredentials.Add(new EmployeeCredential { EmployeeId = id,SecurityBadgeId=guid,Fingerprint=bt});
                securityContext.SaveChanges();
            }
        }




        /*   public bool IsAuthorized(string doorName, string employee)
           {
               using (BuildingSecurityContext securityContext = new BuildingSecurityContext())
               {
                   bool Authorized = false;
                   var room = securityContext.Doors.SingleOrDefault(x => x.Room == doorName);
                   var name = securityContext.Employees.SingleOrDefault(x => x.Name == employee);
                   var roomId = room.DoorId;
                   var roomRanking = securityContext.DoorSecurityLevels.Find(roomId);
                   var empRanking = from ranking in securityContext.EmployeeAccessRights
                                    where ranking.EmployeeId == name.EmployeeId
                                    select ranking.EmployeeSecurityLevelRanking;
                   //int EmpRanking = Convert.ToInt32(empRanking);
                   string a = empRanking.ToString();
                   decimal b = Math.Round(decimal.Parse(a) / 100000, 0) * 100000;
                   if (b >= roomRanking.DoorSecurityLevelRanking)
                   {
                       Authorized = true;
                   }
                   else
                   {
                       Authorized = false;
                   }
                   LogAuthorizationAttempt(roomId, Authorized);
                   return Authorized;
               }*/
    }
    }

    
        
    



