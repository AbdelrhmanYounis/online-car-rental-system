using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.Models
{
    public class History_OfBlock
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("customer_id")]
        public virtual Customer customer { get; set; }
        public int customer_id { get; set; }

        
        [Column(TypeName = "date")]
        public DateTime end_block { get; set; }

        public int block_duration { get; set; }
    }
}