using OnlineCarRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.ViewModel
{
    public class CarCategory
    {
        public Car car { get; set; }
        public IEnumerable<Category> categories { get; set; }
    }
}