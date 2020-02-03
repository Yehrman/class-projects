namespace TheBigTaco
{
  public  class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Password { get; set; }
        public int FailedPasswordCount { get; set; }
        public long PhoneNumber { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsShiftLeader { get; set; }
    }
  
}
