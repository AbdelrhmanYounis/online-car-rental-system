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
    public class CarController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult add_car()
        {
            var categories_db = db.Categories.ToList();
            CarCategory cc = new CarCategory
            {
                categories = categories_db

            };
            return PartialView(cc);
        }
        /******************************************************************************/
        [HttpPost]
        public ActionResult Image_File_Name(HttpPostedFileBase upload)
        {
            var result = false;
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/uploads/car"), upload.FileName);
                upload.SaveAs(path);
                ImageUploaded.image_file_name = upload.FileName;
                result = true;

            }
            else
                ImageUploaded.image_file_name = null;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /******************************************************************************/
        [HttpPost]
        public ActionResult add_car(CarCategory cc)
        {
            var result = false;
            if (ModelState.IsValid)
            {

                cc.car.Image = ImageUploaded.image_file_name;
                db.Cars.Add(cc.car);
                db.SaveChanges();
                result = true;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /*******************************************************************************************/
        
        public ActionResult retrieve_cars(int user_role_id)
        {
            var cars_db = db.Cars.ToList();
            var categories_db = db.Categories.ToList();
            CarsCategory cc = new CarsCategory
            {
                categories = categories_db,
                cars = cars_db,
                UserRoleId=user_role_id
            };
            return View(cc);
        }
        /*******************************************************************************************/
        [HttpGet]
        public ActionResult edit_car(int id)
        {

            var car_selected = db.Cars.SingleOrDefault(x => x.Id == id);
            if (car_selected == null)
            {
                return View("NotFound");
            }

            var categories_db = db.Categories.ToList();
            CarCategory cc = new CarCategory
            {
                categories = categories_db,
                car = car_selected
            };
            return PartialView(cc);

        }
        /*******************************************************************************************/
        [HttpPost]
        public ActionResult edit_car(CarCategory cc)
        {
            var result = false;
            if (ModelState.IsValid)
            {

                var car_db = db.Cars.SingleOrDefault(c => c.Id == cc.car.Id);
                if (ImageUploaded.image_file_name != null)
                {
                    string old_image = Path.Combine(Server.MapPath("~/uploads/car"), car_db.Image);
                    System.IO.File.Delete(old_image);
                    car_db.Image = ImageUploaded.image_file_name;
                }
                else

                    car_db.car_category_id = cc.car.car_category_id;
                car_db.Model = cc.car.Model;
                car_db.Color = cc.car.Color;
                car_db.NumberOfChair = cc.car.NumberOfChair;
                car_db.PriceOfRent = cc.car.PriceOfRent;

                db.SaveChanges();
                result = true;

            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        /*******************************************************************************************/
        [HttpPost]
        public ActionResult delete_car(int id)
        {
            var result = false;
            var car_deleted = db.Cars.SingleOrDefault(x => x.Id == id);
            if (car_deleted != null)
            {
                string old_image = Path.Combine(Server.MapPath("~/uploads/car"), car_deleted.Image);
                System.IO.File.Delete(old_image);
                db.Cars.Remove(car_deleted);

                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /*******************************************************************************************/
        [HttpPost]
        public ActionResult transform_car_state(int id)
        {
            var result = 0;
            var car_selected = db.Cars.SingleOrDefault(x => x.Id == id);
            if (car_selected != null)
            {
                if (car_selected.IsAvialable)
                {
                    car_selected.IsAvialable = false;

                }
                else
                {
                    car_selected.IsAvialable = true;
                    result = 1;
                }
                db.SaveChanges();

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}