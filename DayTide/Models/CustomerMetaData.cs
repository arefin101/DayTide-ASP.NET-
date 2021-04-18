﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayTide.Models
{
    public class CustomerMetaData
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
       [Required]
        public string Address { get; set; }
        [Required]
        public string Picture { get; set; }
    }
}