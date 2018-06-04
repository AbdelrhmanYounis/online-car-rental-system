using OnlineCarRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.ViewModel
{
    public class my_profile
    {
        public User my_user { get; set; }
        public Customer my_customer { get; set; }
        public IEnumerable<Category> my_categories { get; set; }
    }
}