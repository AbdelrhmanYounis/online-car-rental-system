using OnlineCarRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCarRentalSystem.ViewModel
{
    public class Renting
    {
        public int RentCarId { get; set; }
        public IEnumerable<HistoryOfRent> histories { get; set; }
    }
}