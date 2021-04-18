using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class UserMetaData
    {
        public string UserId { get; set; }
        
        public string Password { get; set; }
        
        public string Type { get; set; }
        
        public string Status { get; set; }
    }
}