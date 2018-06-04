using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineCarRentalSystem.Models;

namespace OnlineCarRentalSystem.ViewModel
{
    public class UsersRoles
    {
        public IEnumerable<User> users { get; set; }
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<Role> roles { get; set; }
        public IEnumerable<History_OfBlock> historyofblock { get; set; }
    }
}