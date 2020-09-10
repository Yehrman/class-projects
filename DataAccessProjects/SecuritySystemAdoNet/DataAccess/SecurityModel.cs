using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
public   class SecurityModel:EmployeeModel
    {
  public string Credential { get; set; }
  public DateTime AccessAttempt { get; set; }

        public int AccessHistoryId { get; set; }
        public int DoorId { get; set; }
       public string Door { get; set; }
        public bool Result { get; set; }
    }
}
