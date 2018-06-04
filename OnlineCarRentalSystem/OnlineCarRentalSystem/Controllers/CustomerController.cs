
using OnlineCarRentalSystem.Models;
using OnlineCarRentalSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace OnlineCarRental.Controllers
{
    public class CustomerController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
     
        /*******************************************************************************************/
        [HttpGet]
        public ActionResult car_history(int id)
        {
            Renting rc = new Renting();

            var car_history_db = db.HistiryOfRent.Where(x => x.car_id == id).ToList();
            if (car_history_db.Count() > 0)
            {
                rc.histories = car_history_db;
                rc.RentCarId = id;
               
            }
            else
            {
                   rc.RentCarId = id;

              }
            return PartialView(rc);

        }
        /****************************************************************************/
        [HttpPost]
        public ActionResult RentCar(int id,DateTime begin,int day_num)
        {
            var result = 1;
            if (DateTime.Parse(begin.ToShortDateString()) > DateTime.Parse(@DateTime.Now.AddDays(60).ToShortDateString()))
            {
                result = -2;
            }
            else if (DateTime.Parse(begin.ToShortDateString()) >= DateTime.Parse(@DateTime.Now.ToShortDateString()))
            {

                var car_history_db = db.HistiryOfRent.Where(x => x.car_id == id).ToList();
                if (car_history_db != null)
                {
                    var num_history_rent = car_history_db.Count();

                    foreach (var item in car_history_db)
                    {
                        if (DateTime.Parse(item.begin_rent.ToShortDateString()) < DateTime.Parse(@DateTime.Now.ToShortDateString()))

                            continue;

                        if (
                           (/********h1********d1*********h2**********/
                            DateTime.Parse(begin.ToShortDateString()) >= DateTime.Parse(item.begin_rent.ToShortDateString()) &&
                            DateTime.Parse(begin.ToShortDateString()) <= DateTime.Parse(item.end_rent.ToShortDateString())
                            ) ||
                            (/********h1********d2*********h2**********/
                            DateTime.Parse(begin.AddDays(day_num).ToShortDateString()) >= DateTime.Parse(item.begin_rent.ToShortDateString()) &&
                            DateTime.Parse(begin.AddDays(day_num).ToShortDateString()) <= DateTime.Parse(item.end_rent.ToShortDateString())
                            ) ||
                            (/********d1********h1*********d2**********/
                            DateTime.Parse(begin.ToShortDateString()) <= DateTime.Parse(item.begin_rent.ToShortDateString()) &&
                            DateTime.Parse(begin.AddDays(day_num).ToShortDateString()) >= DateTime.Parse(item.begin_rent.ToShortDateString())
                            )
                            )
                        {
                            result = -3;
                            break;
                        }

                    }
                }
            }
            else
            {
                result = -2;
            }
            if (result==1)
            {
                //go to payment
               result= Payment(id,begin,day_num);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /****************************************************************************/

        public int Payment(int car_rent,DateTime start_rent,int days_num)
        {
            var returner = -1;
            var customer_login_id = (Int32.Parse(Session["UserId"].ToString()));
            var customer = db.Customers.SingleOrDefault(x => x.userId == customer_login_id);
            var car_db = db.Cars.SingleOrDefault(q => q.Id == car_rent);
            var discount = days_num / 5;
            var TotalCost = (car_db.PriceOfRent*days_num)-(car_db.PriceOfRent*discount);
           
            if (customer != null)
            {
                if (customer.account >= TotalCost)
                {
                    customer.account -= TotalCost;
                    db.SaveChanges();
                    car_db.IsAvialable = false;
                    db.SaveChanges();
                    returner = customer.account;
                    var r = customer.user.Email;
                    HistoryOfRent HOFR = new HistoryOfRent
                    {
                        customer_id = customer.Id,
                        car_id = car_rent,
                        begin_rent = start_rent,
                        end_rent = start_rent.AddDays(days_num),
                        discount_rent = discount
                    };
                    db.HistiryOfRent.Add(HOFR);
                    db.SaveChanges();
                    contact(r, "Your Rent Success and Your Accout Is " + customer.account);
                    
                }
            }
            return returner;
        }
        /****************************************************************************/
        [HttpPost]
        public void contact(string email, string message)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("salamaahmedelsayed1997@gmail.com", "01016768183");
            mail.From = new MailAddress("salamaahmedelsayed1997@gmail.com");
            mail.To.Add(new MailAddress(email));
            
            mail.Subject = "Hello My Dear";
            mail.Body = message;

            var smtpClint = new SmtpClient("smtp.gmail.com", 587);
            smtpClint.EnableSsl = true;
            smtpClint.Credentials = loginInfo;
            smtpClint.Send(mail);
           
        }
    }
}