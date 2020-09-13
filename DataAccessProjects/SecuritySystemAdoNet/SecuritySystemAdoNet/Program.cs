using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemAdoNet
{
    public class Program : DataConnection
    {
        
        public static EmployeeRepository eRepository = new EmployeeRepository();
        public static SecurityRepository sRepository = new SecurityRepository();
        static DateTime DateTimeConverter(string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            return dateTime;
        }
        static string Prompt(string val)
        {
            Console.WriteLine("Please type in the " + val);
            Console.Write(">");
            return Console.ReadLine();
        }
        void FindEmployeeByName()
        {

            var name = Prompt("Name");
            var match = eRepository.ReadEmployeeByName(name);
            foreach (var item in match)
            {
                Console.WriteLine($"{item.EmployeeId} {item.FirstName} {item.LastName}");
            }

        }
        void FindEmployeeById()
        {

            var Id = Prompt("Id");
            int Pk = int.Parse(Id);
            var match = eRepository.ReadEmployeeByPk(Pk);
            foreach (var item in match)
            {
                Console.WriteLine($"{item.EmployeeId} {item.FirstName} {item.LastName}");
            }
        }
        void GetEmployeeByNameUsingDataTable()
        {
            var name = Prompt("Name");
            var match = eRepository.ReadEmployeeByNameUsingDataTable(name);
            foreach (var item in match)
            {
                Console.WriteLine($"{item.EmployeeId} {item.FirstName}  {item.LastName}");
            }
        }
        void GetEmployeeByIdUsingDataTable()
        {
            var id = Prompt("id");
            int num = Convert.ToInt32(id);
            var match = eRepository.ReadEmployeeByPkUsingDataTable(num);
            foreach (var item in match)
            {
                Console.WriteLine($"{item.EmployeeId} {item.FirstName}  {item.LastName}");
            }
        }
        void CreateEmployee()
        {
            EmployeeModel model = new EmployeeModel();

            model.FirstName = Prompt("first name of the employee");
            model.LastName = Prompt("last name of employee");
            eRepository.CreateEmployee(model);

        }
        void DeleteEmployee()
        {

            var Id = Prompt("Id of the employee to delete");
            int Pk = int.Parse(Id);
            eRepository.DeleteEmployee(Pk);

        }
        void AddCredential()
        {
            SecurityModel model = new SecurityModel();
            model.Credential = Prompt("name of credential");
            sRepository.AddCredential(model);
        }
        void GetHistory()
        {
            string From = Prompt("The begining date");
            string To = Prompt("The end date");
            var DateF = DateTimeConverter(From);
            var DateT = DateTimeConverter(To);
            var match = sRepository.GetActivity(DateF, DateT);
            /*           SqlCommand cmd = new SqlCommand(@"select ah.AccessHistoryId,ah.AttemptDate,e.EmployeeId, e.FirstName,e.LastName,d.DoorId,d.RoomName,ah.Result from AccessHistory ah join Employee
e on ah.EmployeeId = e.EmployeeId join Door d on ah.DoorId = d.DoorId", Sqlconn); 
*/

            foreach (var item in match)
            {
                Console.WriteLine($"{item.AccessHistoryId}   {item.AccessAttempt}  {item.EmployeeId}  {item.FirstName} {item.LastName} {item.DoorId} {item.Door} {item.Result}");
            }
        }
        /* SqlCommand cmd = new SqlCommand(@"select ah.AccessHistoryId,ah.AttemptDate,e.EmployeeId, e.FirstName,e.LastName,ah.Result from AccessHistory ah join Employee
e on ah.EmployeeId = e.EmployeeId ", Sqlconn);*/
        void GetDoorHistory()
        {
            string From = Prompt("The begining date");
            string To = Prompt("The end date");
            string DoorId = Prompt("the door id");
            int Id = Convert.ToInt32(DoorId);
            var DateF = DateTimeConverter(From);
            var DateT = DateTimeConverter(To);
            var Match = sRepository.GetDoorActivity(DateF, DateT, Id);
            foreach (var item in Match)
            {
                Console.WriteLine($"{item.AccessHistoryId}   {item.AccessAttempt}  {item.EmployeeId}  {item.FirstName} {item.LastName}  {item.Result}");
            }
        }
     
        static void Main(string[] args)
        {

            using (Program p = new Program())
            {
                while (true)
                {
                    Console.WriteLine("To deal with employees type emp.To deal with security type sec");
                    string Select = Console.ReadLine();
                    if (Select == "emp")
                    {
                        Console.WriteLine("To read data type read to add a employee type add to delete a employee press del");
                        string Input = Console.ReadLine();
                        if (Input == "read")
                        {
                            Console.WriteLine("Type store to store the data or any key to read without storing");
                            string typeOfRead = Console.ReadLine();
                            if (typeOfRead == "store")
                            {
                                Console.WriteLine("Type name to search by name or id to serch by id");
                                string Search = Console.ReadLine();
                                if (Search == "name")
                                {
                                    p.GetEmployeeByNameUsingDataTable();
                                }
                                if(Search=="id")
                                {
                                    p.GetEmployeeByIdUsingDataTable();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Type name to search by name or id to serch by id");
                                string Search = Console.ReadLine();
                                if (Search == "name")
                                {
                                    p.FindEmployeeByName();
                                }
                                if (Search == "id")
                                {
                                    p.FindEmployeeById();
                                }
                            }
                            if (Input == "add")
                            {
                                p.CreateEmployee();
                            }
                            if (Input == "del")
                            {
                                p.DeleteEmployee();
                            }
                        }
                        if (Select == "sec")
                        {
                            Console.WriteLine("To add a credential type cred.To see access history type ahist.To get history for a particular door type doorhist");
                            string Search = Console.ReadLine();
                            if (Search == "cred")
                            {
                                p.AddCredential();
                            }
                            if (Search == "ahist")
                            {
                                p.GetHistory();
                            }
                            if (Search == "doorhist")
                            {
                                p.GetDoorHistory();
                            }
                        }

                    }
                }
            }
        }
    }
}