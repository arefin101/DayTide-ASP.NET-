using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class CartViewModelDemo
    {
        
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantiry { get; set; }
        public Nullable<double> Price_unit_ { get; set; }

        public string ProductName { get; set; }
        public string Picture { get; set; }


        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

        public virtual Cart Cart { get; set; }
    }
}