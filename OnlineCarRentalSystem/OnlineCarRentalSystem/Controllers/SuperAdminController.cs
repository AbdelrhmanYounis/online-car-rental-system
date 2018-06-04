using OnlineCarRentalSystem.Models;
using OnlineCarRentalSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCarRentalSystem.Controllers
{
    public class SuperAdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult retrieve_users()
        {
            var roles_db = db.Roles.ToList();
            var users_db = db.Users.ToList();
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
        }
        /*******************************************************************************************/
        [HttpGet]
        public ActionResult add_admin()
        {
            return PartialView();

        }
        /*******************************************************************************************/
        [HttpPost]
        public ActionResult add_admin(User new_admin)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                new_admin.Image = ImageUploaded.image_file_name;
                new_admin.RoleTypeId = 2;

                db.Users.Add(new_admin);
                db.SaveChanges();
                //result=SuperAdminRoleId
                result = 1;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        /*******************************************************************************************/
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
        /*****************************************************************************************/
        [HttpPost]
        public ActionResult super_admin(int id)
        {
            var result = "null";
            var admin_selected = db.Users.SingleOrDefault(x => x.Id == id);
            var super_admin_selected = db.Users.SingleOrDefault(x => x.RoleTypeId == 1);
            if (admin_selected != null)
            {

                if (super_admin_selected != null)
                {
                    string old_image = Path.Combine(Server.MapPath("~/uploads/user"), super_admin_selected.Image);
                    System.IO.File.Delete(old_image);
                    db.Users.Remove(super_admin_selected);
                }

                admin_selected.RoleTypeId = 1;
                db.SaveChanges();
                result = Session["UserName"].ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /************************************************************************************************/
        public ActionResult search_user(string search_text)
        {

            var result = db.Users.Where(x => x.Address.Contains(search_text) ||
                                            x.Email.Contains(search_text) ||
                                            x.Name.Contains(search_text) ||
                                            x.Phone.Contains(search_text)

            ).ToList();
            var roles_db = db.Roles.ToList();
            var customers_db = db.Customers.ToList();
            UsersRoles ur = new UsersRoles
            {
                users = result,
                customers = customers_db,
                roles = roles_db
            };

            return PartialView("search_user", ur);


        }

        /************************************************************************************************/
        [HttpPost]
        public ActionResult delete_user(int id)
        {
            var result = false;
            var user_deleted = db.Users.SingleOrDefault(x => x.Id == id);
            if (user_deleted != null)
            {
                if (user_deleted.RoleTypeId == 3)
                {
                    var customer_deleted = db.Customers.SingleOrDefault(x => x.userId == id);
                    if (customer_deleted != null)
                    {
                        string old_image = Path.Combine(Server.MapPath("~/uploads/user"), user_deleted.Image);
                        System.IO.File.Delete(old_image);
                        db.Customers.Remove(customer_deleted);
                    }
                }
                db.Users.Remove(user_deleted);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}