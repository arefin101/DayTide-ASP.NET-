using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class NoticeMetaData
    {
        [Required]
        public string Massage { get; set; }
    }
}