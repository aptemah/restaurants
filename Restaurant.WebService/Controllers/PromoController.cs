using Intouch.Core;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class PromoController : BaseController
    {
        public JsonResult PromoList(int pointId)
        {
            var promoList = db.RestAdvertisements.Where(a => a.RestPoint.Point.Id == pointId).Select(a => new
            {
                Id = a.Id,
                Name = a.Name,
                Price = a.Price,
                Description = a.Description,
                Link = a.Link,
                FB = a.FbLikeLink,
                VK = a.VkLikeLink,
                TW = a.TwRetwitLink,
                Image = a.Photo
            });
            return Json(new { PromoList = promoList}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreatePromo(string name, string price, string description, string link, string vk, string fb, string tw, int point)
        {
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
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 403;
                return Json(new { Status = "Вы не загрузили изображение." }, JsonRequestBehavior.AllowGet);
            }
            var newPromo = new RestAdvertisement
            {
                Name = name,
                Description = description,
                Price = Convert.ToDecimal(price),
                Link = link,
                Photo = fileName,
                VkLikeLink = vk,
                FbLikeLink = fb,
                TwRetwitLink = tw,
                RestPoint = db.RestPoints.Single(p=>p.Point.Id == point)
            };
            db.RestAdvertisements.Add(newPromo);
            db.SaveChanges();
            return Json (new { Status = "Ok"}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditPromo(int id, string name, string description, string price, string link, string vk, string fb, string tw)
        {
            var currPromo = db.RestAdvertisements.Single(a => a.Id == id);
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
                    currPromo.Photo = fileName;
                }
            }
            if (name != null)
                currPromo.Name = name;
            if (description != null)
                currPromo.Description = description;
            if (price != null)
                currPromo.Price = Convert.ToDecimal(price);
            if (link != null)
                currPromo.Link = link;
            if (vk != null)
                currPromo.VkLikeLink = vk;
            if (fb != null)
                currPromo.FbLikeLink = fb;
            if (tw != null)
                currPromo.TwRetwitLink = tw;
            db.SaveChanges();
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}