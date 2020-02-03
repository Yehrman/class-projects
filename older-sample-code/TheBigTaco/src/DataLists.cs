using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBigTaco
{
    class DataLists
    {
        public static List<MenuItem> MenuItems { get; set; } = GetMenuItems();
        public static List<Employee> Employees { get; set; } = GetEmployees();
        public static List<Order> PendingOrders { get; set; } = new List<Order>();
        public static List<Order> OrderHistory { get; set; } = new List<Order>();
        public static List<(string, string, DateTime)> FailedPasswordLogins { get; set; } = new List<(string, string, DateTime)>();
        public static List<(int, DateTime)> FailedMagCards { get; set; } = new List<(int, DateTime)>();
        //public List<MagneticCard> MagneticCards { get; set; } = GetCardId();

        public static List<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>()
            {
                new MenuItem() { MenuItemID=1, Name="Three Rolled Tacos", Price=3.3M },
                new MenuItem() { MenuItemID=2, Name="Tostado", Price=4.5M },
                new MenuItem() { MenuItemID=3, Name="Chile Relleno", Price=7.99M },
                new MenuItem() { MenuItemID=4, Name="Caesar Salad", Price=4.25M },
                new MenuItem() { MenuItemID=5, Name="Quesodilla", Price=7.99M },
                new MenuItem() { MenuItemID=6, Name="Hot Pepper Salad", Price=3.12M },
                new MenuItem() { MenuItemID=7, Name="Fish Tacos", Price=1M }
            };
        }

        public static List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee() { EmployeeID=1, EmployeeName="John", Password="abc123",PhoneNumber=899-222-3323 },
                new Employee() { EmployeeID=2, EmployeeName="Rick", Password="rickrocks",PhoneNumber=792-232-3245 },
                new Employee() { EmployeeID=3, EmployeeName="Alan", Password="password", IsShiftLeader = true ,PhoneNumber=802-233-3322}
            };
        }
        /*   public static List<MagneticCard> GetCardId()
           {
               return new List<MagneticCard>()
               {
               new MagneticCard() { CardId=859097},
               new MagneticCard(){CardId=868741},
               new MagneticCard(){CardId=789380}
   };*/
        public static List<Order> OrderHistories()
        {
            return new List<Order>()
            {
              new Order(){OrderID=1,CustomerName="Bill",OrderDate=DateTime.Now,Items=new List<MenuItem>(){ new MenuItem() {MenuItemID = 5, Name = "Quesodilla", Price = 7.99M },
            }
        }
        };
        }
    }
}
    

