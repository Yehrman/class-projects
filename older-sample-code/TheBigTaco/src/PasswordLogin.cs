using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBigTaco
{
    class PasswordLogin:DataLists
    {
        //db lookup and response
        public Employee Authenticate(string userName,string password)
        {    
            var found = GetEmployees().FirstOrDefault(x => x.EmployeeName == userName && x.Password == password);
            if (found != null)
            {            
                return found;
            }
            else
            {
                FailedPasswordLogins.Add((userName, password, DateTime.Now));
                return null;
            }
           // return true;
        }
      
    }
}
