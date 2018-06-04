using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.Models
{
    public class User
    { 
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [Display(Name = "Email")]
      //  [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your Name")]
        [Display(Name = "Name")]
       // [RegularExpression(".+\\ .+\\ .+", ErrorMessage = "Please enter at least third Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your Phone Number")]
      //  [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter your Address")]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [ForeignKey("RoleTypeId")]
        public virtual Role RoleType { get; set; }
        public int RoleTypeId { get; set; }
    }
}