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
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var user_from_session = get_login_user();             
            return View(user_from_session);
        }
        /******************************************************************************************/
        [HttpPost]
        public ActionResult Index(User user_update_profile)
        {
            var result = false;
            
            var user_selected = db.Users.SingleOrDefault(x => x.Id.Equals(user_update_profile.Id));
            if (user_selected != null)
            {

                user_selected.Phone = user_update_profile.Phone;
                user_selected.Email = user_update_profile.Email;
                user_selected.Address = user_update_profile.Address;
               
                if (ImageUploaded.image_file_name != null)
                {
                    string old_image = Path.Combine(Server.MapPath("~/uploads/car"), user_selected.Image);
                    System.IO.File.Delete(old_image);
                    user_selected.Image = ImageUploaded.image_file_name;
                }
                if (user_update_profile.Password != null)
                {
                    user_selected.Password = user_update_profile.Password;
                }
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /*******************************************************************************************/
     public User get_login_user()
        {
            var login_user_id = (Int32.Parse(Session["UserId"].ToString()));
            var user_from_session = db.Users.SingleOrDefault(x => x.Id.Equals(login_user_id));
            return user_from_session;
        }
        /*******************************************************************************************/
        [HttpPost]
        public ActionResult Image_Profile_Name(HttpPostedFileBase upload)
        {
            var result = false;
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/uploads/user"), upload.FileName);
                upload.SaveAs(path);
                ImageUploaded.image_file_name = upload.FileName;
                result = true;

            }
            else
                ImageUploaded.image_file_name = null;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /************************************************************************************************/
        public ActionResult search(string search_text,int UserRoleId)
        {

            var result = db.Cars.Where(x => x.Color.Contains(search_text) ||
                                            x.Model.Equals(search_text) ||
                                            x.NumberOfChair.ToString().Equals(search_text) ||
                                            x.PriceOfRent.ToString().Equals(search_text) ||
                                            x.IsAvialable.ToString().Equals(search_text)

            ).ToList();

           
            var model = db.Categories.ToList();
          
            CarsCategory cc = new CarsCategory
            {
                categories =model,
                cars = result,
                UserRoleId=UserRoleId
            };

            return PartialView("partial_search", cc);


        }
       
    }
}