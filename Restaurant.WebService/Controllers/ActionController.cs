using Intouch.Core;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class ActionController : BaseController
    {
        public JsonResult Action(int pointId)
        {
            var actionList = db.RestArticles.Where(a => a.RestPoint.Point.Id == pointId).ToList().Select(s => new
            {
                Id = s.Id,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                DateStart = s.DateStart,
                DateStartYear = s.DateStart.Year,
                DateStartMonth = s.DateStart.Month,
                DateStartDay = s.DateStart.Day,
                DateStartHour = s.DateStart.Hour,
                DateStartMin = s.DateStart.Minute,
                Description = s.Description,
                Name = s.Name,
                Image = s.Image
            }).OrderBy(d => d.DateStart);
            return Json( new { ActionList = actionList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateAction(string name, string description, string date, int point)
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
               
            var newAction = new RestArticle
            {
                Name = name,
                Description = description,
                DateStart = Convert.ToDateTime(date),
                DateCreate = DateTimeOffset.UtcNow,
                Image = fileName,
                RestPoint = db.RestPoints.Single(p => p.Point.Id == point)
            };
            db.RestArticles.Add(newAction);
            db.SaveChanges();
            return Json (new { Status = "Ok"}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditAction(string name, string description, string date, int id)
        {
            var currAction = db.RestArticles.Single(a => a.Id == id);
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
                    currAction.Image = fileName;
                }
            }            
            
            if (name != null)
                currAction.Name = name;
            if (description != null)
                currAction.Description = description;
            if (date != null)
                currAction.DateStart = Convert.ToDateTime(date);
            db.SaveChanges();
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}