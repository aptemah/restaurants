using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class RestaurantController : BaseController
    {
        public JsonResult Restaurant(int pointId)
        {
            var restaurantData = db.RestPoints.Single(p=>p.Point.Id == pointId);
            var restaurant = new
            {
                Name = restaurantData.Name,
                Phone = restaurantData.Phone,
                Kitchen = restaurantData.Kitchen,
                Address = restaurantData.Address,
                Logo = restaurantData.Logo,
                Email = restaurantData.Email,
                Description = restaurantData.Description,
                Price = restaurantData.AveragePrice,
                Time = restaurantData.WorkTime,
                Skype = restaurantData.Skype,
                EmailFeedback = restaurantData.EmailToFeedback,
                EmailReservation = restaurantData.EmailToReservation
            };
            return Json (new { Restaurant = restaurant}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditRestaurant(int id, string name, string phone, string kitchen, string address, string mail, string description, string price, string time, string skype, string feedback, string reservation)
        {
            var restaurant = db.RestPoints.Single(p=>p.Point.Id == id);
            var fileName = "";
            HttpPostedFileBase file;
            if (Request.Files.Count > 0)
            {
                file = Request.Files[0] as HttpPostedFileBase;
                if (file != null && file.ContentLength > 0)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                    var fileExt = Path.GetExtension(file.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        HttpContext.Response.StatusCode = 403;
                        return Json(new { Status = "Неверный тип. Поддерживаются только jpg, jpeg, png форматы." }, JsonRequestBehavior.AllowGet);
                    }

                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                    restaurant.Logo = fileName;
                }
            }
            if (name != null)
                restaurant.Name = name;
            if (phone != null)
                restaurant.Phone = phone;
            if (kitchen != null)
                restaurant.Kitchen = kitchen;
            if (address != null)
                restaurant.Address = address;
            if (mail != null)
                restaurant.Email = mail;
            if (description != null)
                restaurant.Description = description;
            if (price != null)
                restaurant.AveragePrice = price;
            if (time != null)
                restaurant.WorkTime = time;
            if (skype != null)
                restaurant.Skype = skype;
            if (feedback != null)
                restaurant.EmailToFeedback = feedback;
            if (reservation != null)
                restaurant.EmailToReservation = reservation;
            db.SaveChanges();
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}