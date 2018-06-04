using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please insert Category")]
        public string category { get; set; }

        [Display(Name = "Image")]
        
        public string Image { get; set; }

        [Display(Name = "description")]
        public string description { get; set; }

        
    }
}