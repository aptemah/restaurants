using Intouch.Core;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class ChatController : BaseController
    {
        //коннект к беседе
        public JsonResult ConnectToConversation(Guid sessionId, int pointId)
        {
            var conversation = db.Conversations.SingleOrDefault(c => c.Point.Id == pointId);
            if (conversation == null)
            {
                conversation = new Conversation { Point = db.Points.Find(pointId), Created = DateTimeOffset.UtcNow, Name = db.Points.Find(pointId).Name, TypePoint = TypePoint.Restaurant };
                db.Conversations.Add(conversation);
                db.SaveChanges();
            }

            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(true, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var part = db.ChatParticipants.SingleOrDefault(p => p.SUser.Id == user.Id && p.Conversation.TypePoint == TypePoint.Restaurant);
            if (part != null)
            {
                db.ChatParticipants.Remove(part);
                db.SaveChanges();
            }
            var participant = new ChatParticipant { Connect = DateTimeOffset.UtcNow, SUser = user.SocialUser, Conversation = db.Conversations.SingleOrDefault(c => c.Point.Id == pointId) };
            db.ChatParticipants.Add(participant);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //картинка в сообщении
        public JsonResult AddMessagePhoto()
        {
            var image = Request.Files["file"];
            if (image != null && image.ContentLength > 0 && !string.IsNullOrEmpty(image.FileName))
            {
                var fileName = Guid.NewGuid();
                var filePath = string.Format("{0}\\{1}", ImageServices.ContentPath("message"), fileName.ToString() + ".jpg");
                image.SaveAs(filePath);
                return Json(new { photo = fileName.ToString() + ".jpg"}, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}