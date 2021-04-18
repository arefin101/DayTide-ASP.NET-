using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class Request
    {
        public OrderRequest Order { get; set; }
        public CartBackup Cart { get; set; }
        public Product Product { get; set; }
    }
}