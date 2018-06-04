using OnlineCarRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.ViewModel
{
    public class UserCustomer
    {
        public Customer customer { get; set; }
        public User user { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}