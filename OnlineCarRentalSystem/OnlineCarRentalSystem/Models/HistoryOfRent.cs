
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.Models
{
    public class HistoryOfRent
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("customer_id")]
        public virtual Customer customer { get; set; }
        public int customer_id { get; set; }

        [ForeignKey("car_id")]
        public virtual Car car { get; set; }
        public int car_id { get; set; }

        [Required(ErrorMessage = "Please enter begin rent date")]
      
        [Column(TypeName = "date")]
        public DateTime begin_rent { get; set; }

        [Required(ErrorMessage = "Please enter end rent date")]

        [Column(TypeName = "date")]
        public DateTime end_rent { get; set; }

        public float discount_rent { get; set; }
    }
}