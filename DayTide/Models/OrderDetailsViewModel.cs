using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class OrderDetailsViewModel
    {
        public int ID { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantiry { get; set; }
        public double Price { get; set; }
        //public Nullable<int> OrderId { get; set; }

        public string ProductName { get; set; }

        public int OrderId { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public double Amount { get; set; }
        public string Payment_Type { get; set; }
       // public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Order_Detail Order_Detail { get; set; }
    }
}