using OnlineCarRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCarRentalSystem.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult add_category()
        {
            return View();
        }
        /************************************************************************************************/
        [HttpPost]
        public ActionResult add_category(Category c, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid && upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/uploads/category"), upload.FileName);
                upload.SaveAs(path);
                c.Image = upload.FileName;
                db.Categories.Add(c);
                db.SaveChanges();

                return RedirectToAction("retrieve_categories");
            }

            return View("add_category", c);


        }
        /*******************************************************************************************/
        public ActionResult retrieve_categories()
        {
            return View(db.Categories.ToList());
        }
        /************************************************************************************************/
        [HttpGet]
        public ActionResult edit_category(int id)
        {
            var selected_category = db.Categories.SingleOrDefault(x => x.id == id);
            if (selected_category == null)
                return View("NotFound");
            return View(selected_category);
        }
        /*******************************************************************************************/
        [HttpPost]
        public ActionResult edit_category(Category cat, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var old_image = db.Categories.SingleOrDefault(x => x.id == cat.id).Image;

                string old_image_path = Path.Combine(Server.MapPath("~/uploads/category"), old_image);
                if (upload != null)
                {
                    System.IO.File.Delete(old_image_path);
                    string path = Path.Combine(Server.MapPath("~/uploads/category"), upload.FileName);
                    upload.SaveAs(path);
                    cat.Image = upload.FileName;
                }
                else
                {
                    cat.Image = old_image;
                }
                var category_db = db.Categories.SingleOrDefault(x => x.id == cat.id);
                category_db.category = cat.category;
                category_db.Image = cat.Image;
                category_db.description = cat.description;
                db.SaveChanges();
                return RedirectToAction("retrieve_categories");
            }
            else
            {

                return View("edit_category", cat);
            }
        }
        /*******************************************************************************************/
        [HttpPost]
        public ActionResult delete_category(int id)
        {
            var result = false;
            var category_deleted = db.Categories.SingleOrDefault(x => x.id == id);
            var found_car_categor = db.Cars.Where(x => x.car_category_id == category_deleted.id).Count();
            if (category_deleted != null && found_car_categor == 0)
            {
                string old_image = Path.Combine(Server.MapPath("~/uploads/category"), category_deleted.Image);
                System.IO.File.Delete(old_image);
                db.Categories.Remove(category_deleted);

                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}