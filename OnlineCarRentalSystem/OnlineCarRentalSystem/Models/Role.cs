using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string Type { get; set; }
    }
}