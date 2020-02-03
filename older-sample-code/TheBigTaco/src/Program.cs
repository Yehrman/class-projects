using System;
using System.Collections.Generic;
using System.Linq;

namespace TheBigTaco
{
    class Program : DataLists
    {
        //  static LoginManager LoginManager { get; set; } = new LoginManager(Employees);
        static Employee CurrentEmployee { get; set; }
        static decimal TillBalance { get; set; }


       // static int OrderCount = 0;

        static void Main(string[] args)
        {
            TillBalance = 20M;

          
            LoopingMenu(
             "Employee (p)assword login, (m)agnetic card login,(te)xt message login ,start (n)ew order, show (c)urrent orders, (f)inish order, show (h)istory, (t)ill status, (q)uit",
             new Dictionary<string, Func<bool>>()
             {
                    { "p", EmployeePasswordLogin },
                    { "m", MagCardLogin },
                 {"tx",TextMessageLogin },
                    { "n", AddOrder },
                    { "c", ShowOrders },
                    { "f", FinalizeOrder },
                    { "h", ShowHistory },
                    { "t", ShowTillStatus },
                    { "q", () => false }
             }
         );
        }
            static int ParseToInt(string number)
            {
                var parsedInt = int.Parse(number);
                return parsedInt;
            }
        static void ShowRelevantMenu(int orderId)
        {       
                if(PendingOrders.Any(x=>x.OrderID==orderId))
                {
                var PendingOrder = PendingOrders.FirstOrDefault(x => x.OrderID == orderId);
                Console.WriteLine(PendingOrder.CustomerName + " 's order");
                foreach (var item in PendingOrder.Items)
                {
                    Console.WriteLine($"\t{item.MenuItemID}\t{item.Name}\t{item.Price}");
                }
                }
            else
            {
                foreach (var item in GetMenuItems() )
                {
                    Console.WriteLine($"\t{item.MenuItemID}: {item.Name} - {item.Price:C}");
                }
            }
            
        }
         static void ShowCurrentTotal(IEnumerable<MenuItem> menu, decimal tax)
         {
            
             decimal price = 0;
             foreach (var item in MenuItems)
             {
                 foreach (var menuItem in menu)
                 {
                     if(item.MenuItemID==menuItem.MenuItemID)
                     {
                         price += item.Price;
                     }
                 }
             }
             var TaxTotal = price * tax;
             var Total = price + TaxTotal;         
       
             Console.WriteLine($" The current amount due is Subtotal: {price:C}, Tax {tax*100}:, Total: {Total:C}");            
         }
        static void AddToTill(IEnumerable<MenuItem>menu,decimal tax)
        {
            decimal price = 0;
            foreach (var item in MenuItems)
            {
                foreach (var menuItem in menu)
                {
                    if (item.MenuItemID == menuItem.MenuItemID)
                    {
                        price += item.Price;
                    }
                }
            }
            var TaxTotal = price * tax;
            var Total = price + TaxTotal;
            TillBalance += Total;
            Console.WriteLine(Total+" has been added to the till the current balance is "+TillBalance);
        }
   
        static void AddTotalFromAllOrders(decimal tax)
        {
            decimal SubTotal = 0;
            decimal TaxTotal = 0;
            decimal Total = 0;
            foreach (var item in OrderHistory.OrderByDescending(x=>x.OrderDate))
            {
                decimal orderSubtotal = item.Items.Sum(x => x.Price);
                decimal TaxOnOrder=  orderSubtotal * tax;
                decimal total = orderSubtotal + tax;
                Console.WriteLine($"{item.OrderID} ({item.CustomerName}) - Items: {item.Items.Count}, Total: {total}");
                SubTotal += orderSubtotal;
                TaxTotal += TaxOnOrder;
                Total += total;
            }
            Console.WriteLine($"GRAND TOTALS\tSubtotal: {SubTotal:C}, Tax: {TaxTotal:C}, Total: {Total:C}");
        }
            static bool CheckEmployeelogin(long number)
            {
                return CheckEmployeelogin(x => x.PhoneNumber == number);
            }
            static bool CheckEmployeelogin(string userName, string password)
            {
                return CheckEmployeelogin(x => x.EmployeeName == userName && x.Password == password);
            }
            static bool CheckEmployeelogin(int id)
            {
                return CheckEmployeelogin(x => x.EmployeeID == id);
            }

            static bool CheckEmployeelogin(Func<Employee, bool> func)
            {
                var found = Employees.FirstOrDefault(func);
                if (found != null)
                {
                    CurrentEmployee = found;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        static bool CheckMenuItemById(int id)
        {
            return CheckMenuItem(x => x.MenuItemID == id);
        }
        static bool CheckMenuItemByItemName(string menuItem)
        {
            return CheckMenuItem(x => x.Name == menuItem);
        }
        static bool CheckMenuItem(Func<MenuItem,bool>func)
        {
            var found = MenuItems.FirstOrDefault(func);
            if(found!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            /*My version
            static bool EmployeePasswordLogin()
           {
                //
          //Using prompt to obtain user input
               var employeeName = Prompt("Employee name");
               var password = Prompt("Password");
                PasswordLogin passwordLogin = new PasswordLogin();
                //using passwordLogin.Authenticate to authenticate the user and respond
                var auth = passwordLogin.Authenticate(employeeName, password);
                if (auth != null)
                {
                    //using current employee to set the logged in user
                    Console.WriteLine("hello " + employeeName);
                    auth = CurrentEmployee;
                }
                //double paretheses does type cocerion
                //logging the user
                else
                {
                    Console.WriteLine("Incorrect employee name or password");

                }
                return true;
            }
            */
            //YY Kosbies version
            static bool EmployeePasswordLogin()
            {
                var name = Prompt("Employee name");
                var password = Prompt("Password");
                var login = CheckEmployeelogin(name, password);
                if (login == true)
                {
                    Console.WriteLine("hello " + name);
                }
                else
                {
                    Console.WriteLine("Incorrect employee name or password");
                    FailedPasswordLogins.Add((name, password, DateTime.Now));
                }
                return true;
            }
            static bool MagCardLogin()
            {


                int empid = MagneticCard.ReadCardId();
                var login = CheckEmployeelogin(empid);
                if (login == true)
                {
                    Console.WriteLine($"Employee {empid} logged in via card");
                }
                else
                {
                    Console.WriteLine("Invalid Card id");
                    FailedMagCards.Add((empid, DateTime.Now));
                }
                return true;
            }
            static bool TextMessageLogin()
            {
                var number = Prompt("Phone Number");
                long phone = Convert.ToInt64(number);
                var login = CheckEmployeelogin(phone);
                if (login == true)
                {
                    Console.WriteLine($"Employee logged in via this phone #  {phone}");
                }
                else
                {
                    Console.WriteLine("Invalid Card id");
                    // FailedMagCards.Add((, DateTime.Now));
                }
                return true;
            }

            static bool AddOrder()
            {
                Order order = new Order();
                //) Receive customer name.Knowledge needed-Console.writeline readline
                order.CustomerName = Prompt("Enter customer name");
            //)Set a order id Knowledge needed-call ++ to get the next available number
            var id = order.OrderID = OrderHistory.Max(x => x.OrderID) + 1;
          
                LoopingMenu(
                    "(L)ist items, (a)dd an item, show (c)urrent, (f)inish",
                    new Dictionary<string, Func<bool>>()
                    {
                    {
                        "l",
                        () =>
                        {
                            ShowRelevantMenu(id);
                            return true;
                        }
                    },
                    {
                        "a",
                        () =>
                        {

                            var itemIdString = Prompt("Menu Item #");
                            int itemId=  ParseToInt(itemIdString);
                            var item = CheckMenuItemById(itemId);
                            if (item ==true)
                            {
                                Console.WriteLine("Added to order");
                            }
                            else
                            {
                                Console.WriteLine($"Could not find item #{itemIdString}");
                            }

                            return true;
                        }
                    },
                    {
                        "c",
                        () =>
                        {
                            //)Setting/tracking the current order Knowledge needed foreach the current order
                            Console.WriteLine($"Current items in order #{order.OrderID}");
                            foreach (MenuItem item in order.Items)
                            {
                                Console.WriteLine($"\t({item.MenuItemID} - {item.Name} - {item.Price}");
                            }
                            ShowCurrentTotal(order.Items,0.0875m);
                            return true;
                        }
                    },
                    {
                        "f",
                        () =>
                        {
                  
                           ShowCurrentTotal(order.Items,0.0875m);
                            PendingOrders.Add(order);
                            AddToTill(order.Items,0.0875m);                      
                            return false;
                        }
                    }
                    }
                );

                return true;
            }
        //incomplete and not encapsuleted
            static bool ShowOrders()
            {

            foreach (var item in PendingOrders)
            {
                Console.WriteLine("\t" + item.OrderID + "\t" + item.CustomerName);
            }
                
                
                return true;
            }

            static bool FinalizeOrder()
            {
                var orderIdString = Prompt("Order # to finish");
                var orderId = ParseToInt(orderIdString);
                for (int ixOrder = 0; ixOrder < PendingOrders.Count; ixOrder++)
                {
                    //remove from pending orders add to order history Knowledge needed .add .remove
                    if (PendingOrders[ixOrder].OrderID == orderId)
                    {
                        OrderHistory.Add(PendingOrders[ixOrder]);
                        PendingOrders.RemoveAt(ixOrder);
                        return true;
                    }
                }
                //if order is'nt found tell us
                Console.WriteLine("Couldn't find order #" + orderIdString);

                return true;
            }

            static bool ShowHistory()
            {
            AddTotalFromAllOrders(.0875m);
                return true;
            }

            static bool ShowTillStatus()
            {
                Console.WriteLine($"The till should have {TillBalance:C} in it");
                return true;
            }

            static void LoopingMenu(string menuText, Dictionary<string, Func<bool>> actions)
            {
                while (true)
                {
                    string actionCode = Prompt(menuText);
                    if (actions.ContainsKey(actionCode.ToLower()))
                    {
                        Func<bool> action = actions[actionCode];
                        var @continue = action();

                        if (!@continue) return;
                    }
                    else
                    {
                        Console.WriteLine($"Unrecognized option `{actionCode}`.  Try again.");
                    }
                }
            }

            static string Prompt(string msg)
            {
                Console.WriteLine(msg);
                Console.Write("> ");
                return Console.ReadLine();
            }


        }
    }
