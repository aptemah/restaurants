using Intouch.Core;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Intouch.Restaurant.Controllers
{
    public class CabinetController : BaseController
    {
        //история заказов
        public JsonResult GetOrderHistory(Guid? sessionId, int? userId)
        {
            var user = new RestAppUser();

            if (userId.HasValue)
            {
                user = db.RestAppUsers.SingleOrDefault(u => u.Id == userId);
                if (user == null) return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (sessionId.HasValue)
            {
                var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
                if (session == null) return Json(false, JsonRequestBehavior.AllowGet);

                user = session.RestAppUser;
            }

            var orderHistory = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.RestOrderParts.Any(op => op.ValidPurchase == ValidPurchase.Confirmed) && o.OpenClose == OpenClose.Close).Select(s => new {
                Id = s.Id,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                OrderDate = s.DateCreate,
                OrderSum = s.RestOrderParts.Where(o => o.RestOrder.Id == s.Id & o.ValidPurchase == ValidPurchase.Confirmed).SelectMany(sm => sm.RestOrderProducts).Select(sl => new
                {
                    Price = sl.Price * sl.Quantity
                }).Sum(sm => sm.Price),
                Bonus = db.RestBonusHistorys.Where(b => b.RestOrder.Id == s.Id //&& b.RestAppUser.Id == user.Id 
                    ).Select(sl => new
                    {
                        Mount = sl.Mount,
                        Operation = sl.PlusMinus
                    }).FirstOrDefault()


            }).OrderBy(o => o.OrderDate);
            return Json(new { orderHistory = orderHistory }, JsonRequestBehavior.AllowGet);
        }

        //инфа о юзере
        public JsonResult GetUserInfo(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = db.RestAppUsers.Where(u => u.Id == session.RestAppUser.Id).Select(s => new {
                Id = s.Id,
                Name = s.Name,
                Phone = s.Phone,
                Email = s.Email,
                Age = s.AgeUser,
                CheckUser = s.CheckUser,
                Sex = s.Sex,
                RegistrDate = s.RegistrationDate,
                Photo = s.Photo
            });
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        //редактировать имя
        public JsonResult NameEdit(Guid sessionId, string name)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;
            user.Name = name;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //редактировать мейл
        public JsonResult EmailEdit(Guid sessionId, string email)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;
            user.Email = email;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //сохранить картинку на сервер
        public JsonResult AddUserPhoto()
        {
            var image = Request.Files["file"];
            if (image != null && image.ContentLength > 0 && !string.IsNullOrEmpty(image.FileName))
            {
                ImageObjModel imageObject = ResizeOrigImg(image);
                var fileName = Guid.NewGuid();
                var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("photo"), fileName.ToString() + ".jpg");
                imageObject.Image.Save(filePath);
                return Json(new { photo = fileName.ToString() + ".jpg" }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        //изменить картинку в базе
        public JsonResult PhotoEdit(Guid sessionId, string photo)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;
            user.Photo = photo;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //изменить пароль
        public JsonResult ChangePassword(Guid sessionId, string oldPassword, string newPassword)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;
            using (var md5Hash = MD5.Create())
            {
                if (user.Password != GetMd5Hash(md5Hash, oldPassword))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                user.Password = GetMd5Hash(md5Hash, newPassword);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Json(session.Id, JsonRequestBehavior.AllowGet);
            }
        }

        //отправить смс для изменения 
        public JsonResult SendSms(string phone, Guid sessionId)
        {
            var code = SendCode(phone);
            var result = AddSms(code, sessionId);
            if (result) return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //смс в базу
        public bool AddSms(int code, Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return false;
            var sms = new RestSms { Date = DateTime.UtcNow, Code = code, RestAppUser = db.RestAppUsers.Find(session.RestAppUser.Id), UsedSms = UsedSms.NotUsed };
            db.RestSmses.Add(sms);
            db.SaveChanges();
            return true;
        }

        //изменение телефона
        public JsonResult ChangePhone(int code, Guid sessionId, string newPhone)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);

            var sms = db.RestSmses.Where(u => u.RestAppUser.Id == session.RestAppUser.Id).OrderByDescending(o => o.Date).FirstOrDefault();
            if (sms == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (sms.Code == code)
            {
                sms.UsedSms = UsedSms.Used;
                db.Entry(sms).State = EntityState.Modified;
                db.SaveChanges();

                var user = session.RestAppUser;
                user.Phone = newPhone;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //оставить отзыв
        public JsonResult SendReview(int pointId, Guid sessionId, string comment)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);

            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var review = new RestReview { Date = DateTimeOffset.UtcNow, RestAppUser = user, Comment = comment, RestPoint = point};
            db.RestReviews.Add(review);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //список отзывов
        public JsonResult GetReviewList(int pointId)
        {
            var point = db.RestPoints.SingleOrDefault(p => p.Point.Id == pointId);
            if (point == null) return Json(false, JsonRequestBehavior.AllowGet);

            var review = db.RestReviews.Where(r => r.RestPoint.Id == point.Id).Select(s => new
            {
                Date = s.Date,
                DateYear = s.Date.Year,
                DateMonth = s.Date.Month,
                DateDay = s.Date.Day,
                DateHour = s.Date.Hour,
                DateMin = s.Date.Minute,
                DateSec = s.Date.Second,
                User = s.RestAppUser.Name,
                Comment = s.Comment
            }).OrderBy(o => o.Date).Take(50);
            return Json(review, JsonRequestBehavior.AllowGet);
        }

        //заказ столика
        public JsonResult ReservationTable(int day, int month, int year, int minute, int hour, string phone, int people, string comment, int pointId)
        {
            var restaurant = db.RestPoints.SingleOrDefault(r => r.Point.Id == pointId);
            if (restaurant == null) return Json(false, JsonRequestBehavior.AllowGet);

            var dateReservation = new DateTimeOffset(year, month, day, hour, minute, 0, new TimeSpan(0));
            var reservation = new RestReservation { DateCreate = DateTimeOffset.UtcNow, DateReservation = dateReservation, Comment = comment, People = people, Phone = phone, RestPoint = restaurant };
            db.RestReservations.Add(reservation);
            db.SaveChanges();
            var date = day + "/" + month + "/" + year + " " + hour + ":" + minute;
            var message = "Телефон: " + phone + "\n" + "Время: " + date + "\n" + "Количество людей: " + people + "\n" + "Комментарий: " + comment;
            var result = SendEmail(pointId, "бронирование столика", message, "reserve");
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        //обратная связь
        public JsonResult LeaveFeedback(int pointId, string phone, string email, string comment)
        {
            var restaurant = db.RestPoints.SingleOrDefault(r => r.Point.Id == pointId);
            if (restaurant == null) return Json(false, JsonRequestBehavior.AllowGet);

            var feedback = new RestFeedback { RestPoint = restaurant, Phone = phone, Date = DateTimeOffset.UtcNow, Email = email, Feedback = comment };
            db.RestFeedbacks.Add(feedback);
            db.SaveChanges();

            var message = "Телефон: " + phone + "\n" + "Эл.почта: " + email + "\n" + "Комментарий: " + comment;
            var result = SendEmail(pointId, "обратная связь", message, "feedback");

            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        //отправка на почту
        public string SendEmail(int pointId, string subject, string content, string type)
        {
            var fromAddress = new MailAddress("intouchmedia.dev@gmail.com");
            var rest = db.RestPoints.SingleOrDefault(r => r.Point.Id == pointId);
            if (rest == null) return "false1";

            MailAddress toAddress;

            if (type == "reserve")
            {
                toAddress = new MailAddress(rest.EmailToReservation);
            }
            else
            {
                toAddress = new MailAddress(rest.EmailToFeedback);
            }
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "QzRsvZMP2308")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = content
            })
            {
                try
                {
                    smtp.Send(message);

                    return "true";
                }
                catch (Exception ext)
                {
                    return ext.ToString();
                }
            }
        }

        //инфа о заказе
        public JsonResult OrderInfo(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var order = db.RestOrders.Where(o => o.RestAppUser.Id == user.Id && o.OpenClose == OpenClose.Open).OrderByDescending(d => d.DateCreate).Select(s => new
            {
                Id = s.Id,
                DateCreateYear = s.DateCreate.Year,
                DateCreateMonth = s.DateCreate.Month,
                DateCreateDay = s.DateCreate.Day,
                DateCreateHour = s.DateCreate.Hour,
                DateCreateMin = s.DateCreate.Minute,
                DateCreate = s.DateCreate,
                OpenClose = s.OpenClose,
                OrderNumber = s.OrderNumber,
                TypeOfPayment = s.TypeOfPayment,
                BonusThisTime = s.BonusThisTime,
                WaiterComment = s.WaiterComment,
                Orders = db.RestOrderParts.Where(a => a.RestOrder.Id == s.Id).Select(r => new
                {
                    Id = r.Id,
                    DateYear = r.Date.Year,
                    DateMonth = r.Date.Month,
                    DateDay = r.Date.Day,
                    DateHour = r.Date.Hour,
                    DateMin = r.Date.Minute,
                    Date = r.Date,
                    Comment = r.Comment,
                    CookTime = r.CookTime,
                    ValidPurchase = r.ValidPurchase,
                    TypeOfOrder = r.TypeOfOrder,
                    Products = db.RestOrderProducts.Where(p => p.RestOrderPart.Id == r.Id).Select(p => new
                    {
                        Id = p.Id,
                        Name = p.RestProduct.Name,
                        ProdId = p.RestProduct.Id,
                        Description = p.RestProduct.Description,
                        Weight = p.RestProduct.Weight,
                        Image = (p.RestProduct.Image != null) ? (p.RestProduct.Image) : (p.RestProduct.Category.Image),
                        Quantity = p.Quantity,
                        Price = p.Price,
                    })
                })
            }).FirstOrDefault();
            return Json(order, JsonRequestBehavior.AllowGet);
        }
    }
}