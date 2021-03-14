using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class CategoryMetaData
    {
        [Required]
        public string CategoryName { get; set; }
    }
}