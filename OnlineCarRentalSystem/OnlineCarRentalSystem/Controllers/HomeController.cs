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
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ViewResult Index()
        {
            TransformUserBlockByDate();
            TransformCarStateByDate();
            return View();
        }
        /***************************************************************************/
        private void TransformUserBlockByDate()
        {

            var customer_db = db.Customers.Where(x => x.Isblocked != 0).ToList();
            var history_block_db = db.History_OfBlock.ToList();
            var date_now = @DateTime.Now.ToShortDateString();
            foreach (var customer in customer_db)
            {
                var max_date_of_block = history_block_db.Where(q => q.customer_id == customer.Id)
                                                     .OrderByDescending(w => w.end_block)
                                                     .FirstOrDefault()
                                                     .end_block.ToShortDateString();


                if (DateTime.Parse(max_date_of_block) < DateTime.Parse(date_now))
                {
                    customer.Isblocked = 0;
                    db.SaveChanges();
                }

            }
        }
        /***************************************************************************/
        private void TransformCarStateByDate()
        {
            //var car_db = db.Cars.Where(x => x.IsAvialable == false).ToList();
            //var history_rent_db = db.HistiryOfRent.ToList();
            //var date_now = @DateTime.Now.ToShortDateString();
            //foreach (var x in car_db)
            //{
            //    var max_date_ofrent = history_rent_db.Where(q => q.car_id == x.Id)
            //                                         .OrderByDescending(w => w.end_rent)
            //                                         .FirstOrDefault()
            //                                         .end_rent.ToShortDateString();


            //    if (DateTime.Parse(max_date_ofrent) < DateTime.Parse(date_now))
            //    {
            //        x.IsAvialable = true;
            //        db.SaveChanges();
            //    }

            //}
        }
        /******************************************************************************************/
        [HttpGet]
        public ActionResult Registration()
        {
            UserCustomer uc = new UserCustomer
            {
                categories = db.Categories.ToList()
            };
            
            return PartialView(uc);
        }
        /**************************************************************************/
        [HttpPost]
        public ActionResult Image_Customer_Name(HttpPostedFileBase upload)
        {
            var result = "";
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/uploads/user"), upload.FileName);
                upload.SaveAs(path);
                ImageUploaded.image_file_name = upload.FileName;
                
                result = upload.FileName;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /******************************************************************************/
        [HttpPost]
        public ActionResult Registration(UserCustomer uc) {

            var result = false;
          
            if (ModelState.IsValid)
            {
            uc.user.Image= ImageUploaded.image_file_name;
                uc.user.RoleTypeId = 3;

            db.Users.Add(uc.user);
                db.SaveChanges();
               
                uc.customer.userId = db.Users.Max(c => c.Id);
            db.Customers.Add(uc.customer);
            db.SaveChanges();
                result = true;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /***************************************************************************/
        public ActionResult UserLogin()
        {
            return PartialView();
        }
        /**************************************************************************/
        [HttpPost]
        public JsonResult UserLogin(User user)
        {
            
            var user_login = db.Users.SingleOrDefault(x=>x.Email== user.Email&&x.Password== user.Password);
            var result ="";
            if (user_login != null)
                    {

                Session["UserId"] = user_login.Id;
                Session["UserName"] = user_login.Name;
                Session["UserRoleId"] = user_login.RoleTypeId;
                if (user_login.RoleTypeId != 3)
                        {
                            result=""+1;
                        }

                else if (user_login.RoleTypeId == 3)
                {
                    var CustomerBlocked = db.Customers.SingleOrDefault(q => q.userId == user_login.Id);
                    if (CustomerBlocked.Isblocked > 0)
                    {
                        
                        result = db.History_OfBlock.Where(c => c.customer_id == CustomerBlocked.Id)
                                                .OrderByDescending(w => w.Id)
                                                .FirstOrDefault().end_block.ToShortDateString();
                       

                    }
                    else
                    {
                        result =""+1;
                    }
                    
                        }
            }
                
            
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        /**************************************************************************/

        public ViewResult NotFound()
        {
            return View();
        }
        /******************************************************************/
        public ViewResult admin_page()
        {
            return View();
        }
        /*******************************************************************/
        public ViewResult UserLogout()
        {
            Session.Clear();
            Session.Abandon();
            return View("UserLogin");
        }
    }

}