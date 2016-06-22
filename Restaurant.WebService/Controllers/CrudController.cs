using Intouch.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Intouch.Restaurant.Controllers
{
    public class CrudController : BaseController
    {
        //CRUD menu
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AddCategory(Guid sessionId, int activity, string image, int? catId, string catName, int? parentCat, int? pointId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            if (catId == null)
            {
                if (!parentCat.HasValue) return Json(false, JsonRequestBehavior.AllowGet);
                var restaurant = db.RestPoints.SingleOrDefault(r => r.Point.Id == pointId);
                if (restaurant == null) return Json(false, JsonRequestBehavior.AllowGet);
                var parentCategory = db.RestCategorys.SingleOrDefault(p => p.Id == parentCat);
                if (parentCategory != null)
                {
                    var category = new RestCategory { Name = catName, Activity = (Activity)activity, Image = image, ParentCategory = parentCategory, RestPoint = restaurant };
                    db.RestCategorys.Add(category);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            var category2 = db.RestCategorys.SingleOrDefault(c => c.Id == catId);
            if (category2 == null) return Json(true, JsonRequestBehavior.AllowGet);
            category2.Name = catName;
            category2.Activity = (Activity)activity;
            if (image != "") category2.Image = image;
            db.Entry(category2).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCategory(int catId, Guid sessionId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            using (var dbd = new CoreContext())
            {
                var category = dbd.RestCategorys.SingleOrDefault(c => c.Id == catId);
                if (category == null) return Json(false, JsonRequestBehavior.AllowGet);
                if (category.SubCategories.Count > 0)
                {
                    foreach (var i in category.SubCategories)
                    {
                        DeleteCategory(i.Id, sessionId);
                    }
                    using (var dbs = new CoreContext())
                    {
                        var catNew = dbs.RestCategorys.Find(catId);
                        dbs.RestCategorys.Remove(catNew);

                        dbs.SaveChanges();
                    }
                }
                else
                {
                    db.RestCategorys.Remove(category);
                    db.SaveChanges();
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddProductImage()
        {
            var image = Request.Files["file"];
            if (image == null || image.ContentLength <= 0 || string.IsNullOrEmpty(image.FileName)) return null;

            var fileName = Guid.NewGuid();
            var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("icons/menu"), fileName.ToString() + ".jpg");
            image.SaveAs(filePath);
            return Json(fileName.ToString() + ".jpg", JsonRequestBehavior.AllowGet);
            
        }
        public JsonResult AddProduct(Guid sessionId, int? prodId, string prodName, int number, int activity, decimal prodPrice, string prodDescription, string pict, string prodWeight, int catId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            if (prodId == null)
            {
                var category = db.RestCategorys.SingleOrDefault(c => c.Id == catId);
                if (category == null) return Json(false, JsonRequestBehavior.AllowGet);

                if (pict == "") pict = category.Image;

                var product = new RestProduct { Name = prodName, Activity = (Activity)activity, Number = number, Price = prodPrice, Description = prodDescription, Weight = prodWeight, Category = category, Image = pict };
                db.RestProducts.Add(product);
            }
            else
            {
                var product = db.RestProducts.SingleOrDefault(p => p.Id == prodId);
                if (product == null) return Json(false, JsonRequestBehavior.AllowGet);

                product.Name = prodName;
                product.Price = prodPrice;
                product.Description = prodDescription;
                product.Activity = (Activity)activity;
                product.Number = number;
                if (prodWeight != "") product.Weight = prodWeight;
                db.Entry(product).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteProduct(Guid sessionId, int prodId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var product = db.RestProducts.SingleOrDefault(p => p.Id == prodId);
            if (product == null) return Json(false, JsonRequestBehavior.AllowGet);
            db.RestProducts.Remove(product);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddExcelMenu()
        {
            var file = Request.Files["file"];
            //int point = Convert.ToInt32(Request.Form["pointId"]);
            if (file == null || file.ContentLength <= 0 || string.IsNullOrEmpty(file.FileName)) return Json(false, JsonRequestBehavior.AllowGet);

            var fileName = DateTimeOffset.Now.Day + "." + DateTimeOffset.Now.Month + "." + DateTimeOffset.Now.Year + ".xlsx";
            var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("XLS"), fileName.ToString());
            filePath = FileSave(filePath, file, 1);
            return Json(filePath, JsonRequestBehavior.AllowGet);
        }
        public bool CheckExistFile(string filePath)
        {
            var fileInf = new FileInfo(filePath);
            return fileInf.Exists;
        }
        public string FileSave(string filePath, HttpPostedFileBase file, int i)
        {
            if (!CheckExistFile(filePath))
            {
                file.SaveAs(filePath);
                return filePath;
            }
            filePath = filePath.Remove(filePath.Length - 5) + "_" + i + ".xlsx";
            i++;
            var path = FileSave(filePath, file, i);
            return path;
        }
        //CRUD Article
        public JsonResult AddArticleImage()
        {
            var image = Request.Files["file"];
            if (image == null || image.ContentLength <= 0 || string.IsNullOrEmpty(image.FileName)) return null;

            var fileName = Guid.NewGuid();
            var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("article"), fileName.ToString() + ".jpg");
            image.SaveAs(filePath);
            return Json(fileName.ToString() + ".jpg", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddArticle(Guid sessionId, int pointId, DateTimeOffset dateStart, string description, string name, string image)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var article = new RestArticle { DateStart = dateStart, DateCreate = DateTimeOffset.UtcNow, Description = description, Image = image, Name = name, RestPoint = point };
            db.RestArticles.Add(article);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteArticle(Guid sessionId, int articleId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var article = db.RestArticles.SingleOrDefault(a => a.Id == articleId);
            if (article == null) return Json(false, JsonRequestBehavior.AllowGet);

            db.RestArticles.Remove(article);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //CRUD News
        public JsonResult AddNewsImage()
        {
            var image = Request.Files["file"];
            if (image == null || image.ContentLength <= 0 || string.IsNullOrEmpty(image.FileName)) return null;

            var fileName = Guid.NewGuid();
            var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("news"), fileName.ToString() + ".jpg");
            image.SaveAs(filePath);
            return Json(fileName.ToString() + ".jpg", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddNews(Guid sessionId, int pointId, string smallDescription, string description, string name, string image)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var news = new RestNews { DateCreate = DateTimeOffset.UtcNow, Description = description, SmallDescription = smallDescription, Image = image, Name = name, RestPoint = point };
            db.RestNewses.Add(news);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteNews(Guid sessionId, int newsId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var news = db.RestNewses.SingleOrDefault(a => a.Id == newsId);
            if (news == null) return Json(false, JsonRequestBehavior.AllowGet);

            db.RestNewses.Remove(news);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //CRUD gallery
        public JsonResult AddGalleryImage()
        {
            var image = Request.Files["file"];
            if (image == null || image.ContentLength <= 0 || string.IsNullOrEmpty(image.FileName)) return null;

            var fileName = Guid.NewGuid();
            var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("gallery"), fileName.ToString() + ".jpg");
            image.SaveAs(filePath);
            return Json(fileName.ToString() + ".jpg", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddPhotoToGallery(Guid sessionId, int pointId, string image, string description)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var photo = new RestGallery { Image = image, Description = description, RestPoint = point };
            db.RestGallerys.Add(photo);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteGalleryPhoto(Guid sessionId, int photoId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var photo = db.RestGallerys.SingleOrDefault(p => p.Id == photoId);
            if (photo == null) return Json(false, JsonRequestBehavior.AllowGet);
            db.RestGallerys.Remove(photo);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //CRUD main photo
        public JsonResult AddMainPhoto()
        {
            var image = Request.Files["file"];
            if (image == null || image.ContentLength <= 0 || string.IsNullOrEmpty(image.FileName)) return null;

            var fileName = Guid.NewGuid();
            var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("mainphoto"), fileName.ToString() + ".jpg");
            image.SaveAs(filePath);
            return Json(fileName.ToString() + ".jpg", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddMainPhoto(Guid sessionId, int pointId, string image)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var photo = new RestMainPhoto { Photo = image, RestPoint = point, DateCreate = DateTimeOffset.UtcNow};
            db.RestMainPhotos.Add(photo);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMainPhoto(Guid sessionId, int photoId)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var photo = db.RestMainPhotos.SingleOrDefault(p => p.Id == photoId);
            if (photo == null) return Json(false, JsonRequestBehavior.AllowGet);

            db.RestMainPhotos.Remove(photo);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //Crud restaurant
        public JsonResult ClubEdit(Guid sessionId, int pointId, string description, string clubObject)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var point = db.Points.SingleOrDefault(p => p.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var viewClub = JsonConvert.DeserializeObject<ViewRestModel>(clubObject);

            point.Name = viewClub.clubName;
            point.Latitude = viewClub.Longtitude;
            point.Longitude = viewClub.Latitute;
            db.Entry(point).State = EntityState.Modified;

            var restaurant = db.RestPoints.SingleOrDefault(r => r.Point.Id == pointId);
            if (restaurant == null) return Json(false, JsonRequestBehavior.AllowGet);

            restaurant.Kitchen = viewClub.clubKitchen;
            restaurant.WorkTime = viewClub.clubTime;
            restaurant.Address = viewClub.clubAdress;
            restaurant.Skype = viewClub.clubSkype;
            restaurant.Phone = viewClub.clubPhone;
            restaurant.Email = viewClub.clubEmail;
            restaurant.Description = description;
            restaurant.EmailToFeedback = viewClub.emailToFeedback;
            restaurant.EmailToReservation = viewClub.emailToReservation;            
            db.Entry(restaurant).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddRestLogo()
        {
            var image = Request.Files["file"];
            if (image == null || image.ContentLength <= 0 || string.IsNullOrEmpty(image.FileName)) return null;

            var fileName = Guid.NewGuid();
            var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("icons/logo"), fileName.ToString() + ".jpg");
            image.SaveAs(filePath);
            return Json(fileName.ToString() + ".jpg", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRestLogo(Guid sessionId, int pointId, string logo)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);
            point.Logo = logo;
            db.Entry(point).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //Crud отзыв
        public JsonResult ReviewEdit(Guid sessionId, int reviewId, string comment)
        {
            if (!CheckManager(sessionId)) return Json(false, JsonRequestBehavior.AllowGet);
            var review = db.RestReviews.SingleOrDefault(r => r.Id == reviewId);
            if (review == null) return Json(false, JsonRequestBehavior.AllowGet);

            review.Comment = comment;
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}