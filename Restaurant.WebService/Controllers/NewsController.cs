using Intouch.Core;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class NewsController : BaseController
    {
        public JsonResult News(int pointId)
        {
            var newsList = db.RestNewses.Where(n => n.RestPoint.Point.Id == pointId).Select(s => new
            {
                Id = s.Id,
                DateCreate = s.DateCreate,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                Description = s.Description,
                SmallDescription = s.SmallDescription,
                Name = s.Name,
                Image = s.Image
            }).OrderByDescending(d => d.DateCreate);
            return Json( new { NewsList = newsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OneNews(int newsId)
        {
            var restNews = db.RestNewses.Where(n => n.Id == newsId).Select(s => new
            {
                Id = s.Id,
                DateCreate = s.DateCreate,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                Description = s.Description,
                SmallDescription = s.SmallDescription,
                Name = s.Name,
                Image = s.Image
            }).SingleOrDefault();
            return Json(restNews, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateNews(string name, string smallDescription, string description, int point)
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

            var newNews = new RestNews
            {
                Name = name,
                SmallDescription = smallDescription,
                Description = description,
                DateCreate = DateTimeOffset.UtcNow,
                Image = fileName,
                RestPoint = db.RestPoints.Single(p => p.Point.Id == point)
            };
            db.RestNewses.Add(newNews);
            db.SaveChanges();
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditNews(string name, string description, string smallDescription, int id)
        {
            var currNews = db.RestNewses.Single(a => a.Id == id);
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
                    currNews.Image = fileName;
                }
            }

            if (name != null)
                currNews.Name = name;
            if (description != null)
                currNews.Description = description;
            if (smallDescription != null)
                currNews.SmallDescription = smallDescription;
            db.SaveChanges();
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}