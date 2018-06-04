using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your SNN")]
        [Display(Name = "SNN")]
        public string SNN { get; set; }

        [Required(ErrorMessage = "Please enter your Birthday")]
        [Display(Name = "Birthday")]
        [Column(TypeName ="date")]
        public DateTime Birthday { get; set; }

        [ForeignKey("userId")]
        public virtual User user { get; set; }
        public int userId { get; set; }

        
        [ForeignKey("prefer_car_category_id")]
        public virtual Category prefer_car_category { get; set; }
        [Required(ErrorMessage = "Please select Car Category")]
        [Display(Name = "Prefer Car Category")]
        public int prefer_car_category_id { get; set; }

        public int account { get; set; }

        
        public int Isblocked { get; set; }
    }
}