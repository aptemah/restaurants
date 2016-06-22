using Intouch.Core;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class ProductController : BaseController
    {
        public JsonResult Product()
        {
            var productList = db.RestProducts.ToList().Select(p=> new
            {
                Name = p.Name,
                Number = p.Number,
                Description = p.Description,
                Price = p.Price,
                Weight = p.Weight,
                Image = p.Image
            });
            return Json (new { ProductList = productList}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateProduct(string name, string description, string weight, string price)
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
            var newProduct = new RestProduct
            {
                Name = name,
                Description = description,
                Price = Convert.ToDecimal(price),
                Image = fileName,
                Weight = weight
            };
            db.RestProducts.Add(newProduct);
            db.SaveChanges();
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditProduct(int id, string name, string description, string price, string weight)
        {
            var currProduct = db.RestProducts.Single(a => a.Id == id);
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
                    currProduct.Image = fileName;
                }
            }
            if (name != null)
                currProduct.Name = name;
            if (description != null)
                currProduct.Description = description;
            if (weight != null)
                currProduct.Weight = weight;
            if (price != null)
                currProduct.Price = Convert.ToDecimal(price);
            db.SaveChanges();
            return Json(new { Status = "Ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}