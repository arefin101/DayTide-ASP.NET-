using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class Orders
    {
        public OrderRequest Order { get; set; }
        public CartBackup Cart { get; set; }
    }
}