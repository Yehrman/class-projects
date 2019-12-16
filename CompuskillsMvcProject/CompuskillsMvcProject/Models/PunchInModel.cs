using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MvcProjectDbConn;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

using System.Net;

using Microsoft.AspNet.Identity;
using CompuskillsMvcProject.Models;
namespace CompuskillsMvcProject.Models
{

    public class PunchInOutModel
    {
       [Required]
      public string Client { get; set; }
      public List<Project> Clients { get; set; }
        [Required]
    public string Project { get; set; }
     public List<Project> Projects { get; set; }
    }
   
    }

   

