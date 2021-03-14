using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class ProductMetaData
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public double Buying_Price { get; set; }
        [Required]
        public double Selling_Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Picture { get; set; }
    }
}