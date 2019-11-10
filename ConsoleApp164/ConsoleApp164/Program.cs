using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp164
{
    class Program
    {
        public class Customers
        {
            Random random = new Random();

            public ArrayList CustList = new ArrayList();
            public string[] Documents = { "Teudat Zehut", "Teudat Neche", "Teudat Oleh", "Darkon", "Driver's Licence", "Student Visa", "Worker Visa" };
            public void AddCustomer(string name)
            {
                CustList.Add(name);
            }
            private string GetCustomer(string name)
            {
                for (int i = 0; i < CustList.Count; i++)
                {
                    if (CustList.Contains(name))
                    {
                        return CustList[i].ToString();
                    }

                }
                return null;

            }
            public string[] GetDocs(string name)
            {
                string d = GetCustomer(name);
                int a = random.Next(Documents.Length);
                int b = random.Next(Documents.Length);
                int c = random.Next(Documents.Length);
                string[] docArr = { Documents[a], Documents[b], Documents[c] };
                return docArr;
            }
                     
        }
        class Employees
        {
            Random random = new Random();

            public  ArrayList List = new ArrayList();
            public string[] Documents = { "Teudat Zehut", "Teudat Neche", "Teudat Oleh", "Darkon", "Driver's Licence", "Student Visa", "Worker Visa" };
            public void AddEmployee(string name)
            {
                List.Add(name);
            }
            private string GetEmployee(string name)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (List.Contains(name))
                    {
                        return List[i].ToString();
                    }

                }
                return null;

            }
            public string[] EmployeeGetDocs(string name)
            {
                string d = GetEmployee(name);
                int a = random.Next(Documents.Length);
                int b = random.Next(Documents.Length);
                int c = random.Next(Documents.Length);
                string[] docArr = { Documents[a], Documents[b], Documents[c] };
                return docArr;
              //  List.Add(name);
            }
            
        }

        static void Main(string[] args)
        {

            Customers customers = new Customers();
            customers.AddCustomer("Chaim");
            customers.AddCustomer("Yuval");
            customers.AddCustomer("Shlomo");
            customers.AddCustomer("Abdullah;");
            Employees employees = new Employees();
            employees.AddEmployee("Sari");
            employees.EmployeeGetDocs("Sari");
            Queue queue = new Queue();
            for (int i = 0; i <customers.CustList.Count ; i++)
            {
                queue.Enqueue(customers.CustList[i]);
            }
          void CompareDocs(string EmpName, string CustName)
            {
             string[] a=  ( customers.GetDocs(CustName));
                string[] b = (employees.EmployeeGetDocs(EmpName));
                int Custamount = 0;
               
                for (int i = 0; i < a.Length;i++)
                {
                    for (int j = 0; j < b.Length; j++)
                    {
                        if (a[i] == b[j])
                        {
                            Custamount++;
                        }
                    }
                  
                    }
                if (Custamount == 3)
                {
                    queue.Dequeue();
                    Console.WriteLine(CustName + " can go home"); ;
                }
                else
                {
                    queue.Enqueue(CustName);
                    Console.WriteLine(CustName + " go to back of line");
                    customers.GetDocs(CustName);
                }
            }
            CompareDocs("Sari", "Chaim");
           CompareDocs("Sari", "Yuval");
            CompareDocs("Sari", "Shlomo");
            CompareDocs("Sari", "Abdullah");
            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
            
          
            Console.ReadKey();

        }


    }

}

        

    

