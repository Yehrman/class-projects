using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemAdoNet
{
    class Program
    {
        public static string Prompt(string val)
        {
            Console.WriteLine("Please type in the " + val);
            Console.Write(">");
            return Console.ReadLine();
        }
        static void FindEmployeeByName()
        {
            using (EmployeeRepository repository = new EmployeeRepository())
            {
                var name = Prompt("Name");
                var match = repository.ReadEmployeeByName(name);
                foreach (var item in match)
                {
                    Console.WriteLine($"{item.EmployeeId} {item.FirstName} {item.LastName}");
                }
            }
        }
        static void FindEmployeeById()
        {
            using(EmployeeRepository repository=new EmployeeRepository())
            {
                var Id = Prompt("Id");
                int Pk = int.Parse(Id);
                var match = repository.ReadEmployeeByPk(Pk);
                foreach (var item in match)
                {
                    Console.WriteLine($"{item.EmployeeId} {item.FirstName} {item.LastName}");
                }
            }
        }
        static void CreateEmployee()
        {
            EmployeeModel model = new EmployeeModel();
            using (EmployeeRepository repository = new EmployeeRepository())
            {
              model.FirstName=  Prompt("first name of the employee");
               model.LastName= Prompt("last name of employee");
                repository.CreateEmployee(model);
            }
        }
        static void DeleteEmployee()
        {
            EmployeeModel model = new EmployeeModel();
            using(EmployeeRepository repository=new EmployeeRepository())
            {
                var Id = Prompt("Id of the employee to delete");
                int Pk = int.Parse(Id);
                repository.DeleteEmployee(Pk);
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("To read data type read to add a employee type add to delete a employee press del");
                string Input = Console.ReadLine();
                if(Input=="read")
                {
                    Console.WriteLine("Type name to search by name or id to serch by id");
                    string Search = Console.ReadLine();
                    if(Search=="name")
                    {
                        FindEmployeeByName();
                    }
                    if(Search=="id")
                    {
                        FindEmployeeById();
                    }
                }
                if(Input=="add")
                {
                    CreateEmployee();
                }
                 if(Input=="del")
                {
                    DeleteEmployee();
                }
                //Console.WriteLine("Type Id to select by id and ")
              
            }
        }
    }
}
