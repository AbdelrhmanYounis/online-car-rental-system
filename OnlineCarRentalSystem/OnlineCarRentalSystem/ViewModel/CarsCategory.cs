using OnlineCarRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.ViewModel
{
    public class CarsCategory
    {
        public IEnumerable<Car> cars { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<HistoryOfRent> History_Of_Rent { get; set; }
        public int UserRoleId { get; set; }
    }
}