
using OnlineCarRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.ViewModel
{
    public class CarCustomerCategoryHistoryOfRent
    {
        public IEnumerable<Car> cars { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<HistoryOfRent> histories { get; set; }
    }
}