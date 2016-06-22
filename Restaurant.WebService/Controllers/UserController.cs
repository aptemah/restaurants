using Intouch.Core;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    
    public class UserController : BaseController
    {
        public JsonResult Registration(string name, string password, string phone, string email)
        {
            var user = db.RestAppUsers.SingleOrDefault(r => r.Phone == phone);
            if (user != null)
            {
                var socialUser = db.SocialUsers.SingleOrDefault(s => s.RestUser.Id == user.Id);
                if (socialUser == null)
                {
                    socialUser = new SocialUser
                                 {
                                     RestUser = user,
                                     LastActivityDate = DateTimeOffset.UtcNow
                                 };
                    db.SocialUsers.Add(socialUser);
                    db.SaveChanges();
                }
                using (var md5Hash = MD5.Create())
                {
                    if (password == "" && (user.Password == GetMd5Hash(md5Hash, "")))
                    {
                        var session = AddUserToSession(user);
                        return Json(new { sessionId = session.Id }, JsonRequestBehavior.AllowGet);
                    }
                    if (user.Password == GetMd5Hash(md5Hash, ""))
                    {
                        user.Name = name;
                        user.Phone = phone;
                        user.Email = email;
                        user.RegistrationDate = DateTime.UtcNow;
                        user.Role = RestRole.User;
                        user.CheckUser = CheckUser.Notconfirmed;
                        user.SocialUser = socialUser;
                        user.Photo = "no_photo.png";
                        user.Password = GetMd5Hash(md5Hash, password);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                if (user.CheckUser == CheckUser.Notconfirmed)
                {
                    int code = SendCode(phone);
                    AddSms(code, user.Id);
                    return Json(new { User = user.Id }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { message = "already registered" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var socialUser = new SocialUser();
                using (var md5Hash = MD5.Create())
                {
                    user = new RestAppUser { Name = name, Photo = "no_photo", Password = GetMd5Hash(md5Hash, password), Phone = phone, Email = email, RegistrationDate = DateTime.UtcNow, Role = RestRole.User, CheckUser = CheckUser.Notconfirmed, SocialUser = socialUser };
                }
                socialUser.RestUser = user;
                socialUser.LastActivityDate = DateTimeOffset.UtcNow;
                db.SocialUsers.Add(socialUser);
                db.RestAppUsers.Add(user);
                db.SaveChanges();

                int code = SendCode(phone);
                if (password == "")
                {
                    var payments = new RestPayment { Bonus = 0, RestAppUser = user };
                    db.RestPayments.Add(payments);
                    db.SaveChanges();
                    var session = AddUserToSession(user);
                    return Json(new { sessionId = session.Id }, JsonRequestBehavior.AllowGet);
                }
                AddSms(code, user.Id);
                return Json(new { User = user.Id }, JsonRequestBehavior.AllowGet);
            }
        }
        public void AddSms(int code, int userId)
        {
            var sms = new RestSms { Date = DateTime.UtcNow, Code = code, RestAppUser = db.RestAppUsers.Find(userId), UsedSms = UsedSms.NotUsed };
            db.RestSmses.Add(sms);
            db.SaveChanges();
        }
        public JsonResult RegistrationCheckCode(int code, int userId)
        {
            var sms = db.RestSmses.Where(u => u.RestAppUser.Id == userId).OrderByDescending(o => o.Date).FirstOrDefault();
            if (sms == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (sms.Code == code)
            {
                sms.UsedSms = UsedSms.Used;
                db.Entry(sms).State = EntityState.Modified;
                db.SaveChanges();
                var user = db.RestAppUsers.SingleOrDefault(u => u.Id == userId);
                if (user == null) return Json(false, JsonRequestBehavior.AllowGet);

                user.CheckUser = CheckUser.Сonfirmed;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                var payments = new RestPayment { Bonus = 0, RestAppUser = user };
                db.RestPayments.Add(payments);
                db.SaveChanges();
                var session = AddUserToSession(user);
                return Json(session.Id, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PasswordRecovery(string phone)
        {
            var user = db.RestAppUsers.SingleOrDefault(u => u.Phone == phone);
            if (user == null) return Json(false, JsonRequestBehavior.AllowGet);
            int code = SendCode(phone);
            AddSms(code, user.Id);
            return Json(new { User = user.Id }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RecoveryCheckCode(int code, string phone)
        {
            var sms = db.RestSmses.Where(u => u.RestAppUser.Phone == phone).OrderByDescending(o => o.Date).FirstOrDefault();
            if (sms == null) return Json(false, JsonRequestBehavior.AllowGet);
            if (sms.Code == code)
            {
                sms.UsedSms = UsedSms.Used;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangePassword(string password, string phone)
        {
            var user = db.RestAppUsers.SingleOrDefault(s => s.Phone == phone);
            if (user == null) return Json(false, JsonRequestBehavior.AllowGet);

            using (var md5Hash = MD5.Create())
            {
                user.Password = GetMd5Hash(md5Hash, password);
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CloseSession(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            session.EndDate = DateTimeOffset.UtcNow;
            db.Entry(session).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Login(string phone, string password)
        {
            using (var md5Hash = MD5.Create())
            {
                var user = db.RestAppUsers.SingleOrDefault(u => u.Phone == phone);
                if ((user == null) || (user.Password != GetMd5Hash(md5Hash, password)))
                {
                    //HttpContext.Response.Status = "loginFail";
                    //HttpContext.Response.StatusCode = 403;
                    //HttpContext.ApplicationInstance.CompleteRequest();
                    //return null;
                    return Json(new { message = "fail" }, JsonRequestBehavior.AllowGet);
                }
                var session = db.RestAppSessions.SingleOrDefault(
                            s => s.RestAppUser.Phone == phone && s.EndDate == null);
                if (session != null)
                {
                    return Json(new { Session = session.Id, status = user.Role, point = (user.Role != RestRole.User) ? user.Point.Id : 0 }, JsonRequestBehavior.AllowGet);
                }
                var newSession = AddUserToSession(user);
                return Json(new { Session = newSession.Id, status = user.Role, point = (user.Role != RestRole.User) ? user.Point.Id : 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetUserBonus(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var bonus = db.RestPayments.SingleOrDefault(b => b.RestAppUser.Id == user.Id);
            if (bonus == null) return Json(false, JsonRequestBehavior.AllowGet);
            return Json(bonus.Bonus, JsonRequestBehavior.AllowGet);
        }
    }
}