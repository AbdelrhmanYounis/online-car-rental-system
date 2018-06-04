using OnlineCarRentalSystem.Models;
using OnlineCarRentalSystem.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCarRentalSystem.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        /*******************************************************************************************/
        public ActionResult retrieve_users(int user_role_id)
        {
            var roles_db = db.Roles.Where(q => q.id != 1).ToList();
            var users_db = db.Users.ToList();
            var historyofblock_db = db.History_OfBlock.ToList();
            var customers_db = db.Customers.ToList();

            if (user_role_id == 2)
            {
                users_db = db.Users.Where(q => q.RoleTypeId == 3).ToList();
                roles_db = db.Roles.Where(q => q.id == 3).ToList();
            }

            UsersRoles ucr = new UsersRoles
            {
                users = users_db,
                customers = customers_db,
                roles = roles_db,
                historyofblock = historyofblock_db
            };
            return View(ucr);
        }
        /************************************************************************************************/
        [HttpPost]
        public ActionResult editCustomerAccount(int user_id,int addtionalAccount)
        {
            var result = 0;
            var customer_selected = db.Customers.SingleOrDefault(x => x.userId == user_id);
           if (customer_selected != null)
            {
               
                   customer_selected.account += addtionalAccount;
               
                db.SaveChanges();
                if (Session["UserRoleId"].Equals(1))
                {
                    result = 1;
                }
                else if (Session["UserRoleId"].Equals(2))
                {
                    result = 2;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /************************************************************************************************/
        [HttpPost]
        public ActionResult BlockCustomer(int user_id, int BlockDuration)
        {
            var result = false;
            var customer_selected = db.Customers.SingleOrDefault(x => x.userId == user_id);
            if (customer_selected != null)
            {
                if (BlockDuration != 0)
                {
                    History_OfBlock HB = new History_OfBlock
                    {
                        customer_id = customer_selected.Id,
                        block_duration = BlockDuration,
                        end_block = @DateTime.Now.AddDays(BlockDuration)
                    };
                    db.History_OfBlock.Add(HB);
                    db.SaveChanges();

                    customer_selected.Isblocked = db.History_OfBlock.Max(c => c.Id);
                }
                else {

                    customer_selected.Isblocked = 0;
                }
                try {
                    db.SaveChanges();
                }
                catch
                {
                    //whdyhb
                }
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        /************************************************************************************************/
       public ActionResult search_customer(string search_text)
        {
            var roles_db = db.Roles.ToList();
            var users_db = db.Users.Where(x => (x.Address.Contains(search_text) ||
                                            x.Email.Contains(search_text) ||
                                            x.Name.Contains(search_text) ||
                                            x.Phone.Contains(search_text)) &&
                                            x.RoleTypeId == 3

            ).ToList();

            var historyofblock_db = db.History_OfBlock.ToList();
            var customers_db = db.Customers.ToList();
            UsersRoles ucr = new UsersRoles
            {
                users = users_db,
                customers = customers_db,
                roles = roles_db,
                historyofblock = historyofblock_db
            };
            return View(ucr);

             

            //return PartialView("search_customer", ucr);


        }
        /************************************************************************************************/
        public ActionResult show_report()
        {
            var categories_db = db.Categories.ToList();
            var cars_db = db.Cars.ToList();
            var customers_db = db.Customers.ToList();
            var HistiryOfRent_db = db.HistiryOfRent.ToList();
            CarCustomerCategoryHistoryOfRent cuch = new CarCustomerCategoryHistoryOfRent
            {
                cars = cars_db,
                customers = customers_db,
                categories = categories_db,
                histories = HistiryOfRent_db
            };
            return View(cuch);

        }
    }
}