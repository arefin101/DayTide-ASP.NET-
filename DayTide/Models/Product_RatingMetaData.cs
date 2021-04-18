using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DayTide.Models
{
    public class Product_RatingMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string CustomerId { get; set; }

    }
}