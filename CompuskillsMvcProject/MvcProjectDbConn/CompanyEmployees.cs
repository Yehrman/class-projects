using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProjectDbConn
{
    public class CompanyEmployee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
     
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        //public string FullName
        //{
        //    get
        //    {
        //        return FirstName + " " + LastName;
        //    }
        //    private set { }
        //}
        public string JobTitle { get; set; }

        public virtual Employee Employee { get; set; }
        [DataType(DataType.Currency)]
        public decimal PayRate { get; set; }
        public string Name { get; set; }
    }
}