using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

       
        [ForeignKey("car_category_id")]
       public virtual Category car_category { get; set; }
         [Required(ErrorMessage = "Please select Car Category")]
        [Display(Name = "Category")]
        public int car_category_id { get; set; }

        [Required(ErrorMessage = "Please enter Car Model")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please enter Car Color")]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Please enter Number Of Chairs")]
        [Display(Name = "Number Of Chairs")]
        public int NumberOfChair { get; set; }

        //[Required(ErrorMessage = "Please upload Car Image")]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Please enter Price of Rent")]
        [Display(Name = "Price of Rent")]
        public int PriceOfRent { get; set; }

        
        [Display(Name = "IsAvialable?")]
        public bool IsAvialable { get; set; }
    }
}