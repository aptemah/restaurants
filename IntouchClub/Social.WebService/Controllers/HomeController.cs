using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Intouch.Core;
using Core.ApplicationServices;

namespace Intouch.Social.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/start.html");
        }
        //public JsonResult UserList()
        //{
        //    CoreContext CTX = new CoreContext();

        //    var Users = CTX.KarUsers.Select(u => new 
        //    {
        //        Id = u.Id,
        //        Name = u.User.Name
        //    });

        //    return Json(Users, JsonRequestBehavior.AllowGet);
        //}
        
        //public JsonResult isRead(string mac, int MessageId)
        //{
        //    CoreContext CTX = new CoreContext();

        //    var UserId = PersonalizationService.GetCurrentUserInfo(mac).User.Id;
        //    var SocialUser = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == UserId);

        //    ChatMsgRecipient Recipient = CTX.ChatMsgRecipients.SingleOrDefault(r => r.Message.Id == MessageId && r.Recipient.Id == SocialUser.Id);

        //    Recipient.Recieved = DateTimeOffset.UtcNow;

        //    CTX.SaveChanges();

        //    return Json("isRead", JsonRequestBehavior.AllowGet);

        //}
        
        ///**/

        //public JsonResult CountForMessage(int UserId)
        //{
        //    // всем если сообщение из общего чата
        //    // и тем кто в группе если не общий чат
        //    CoreContext CTX = new CoreContext();

        //    var Conversation = CTX.ChatMsgRecipients.Where(r => r.Recipient.Id == UserId && r.Recieved == null).GroupBy(r => r.Message.Conversation.Id).Select(r => new
        //    {
        //        ConversationId = r.Key,
        //        Count = r.Count(),
        //        OponentName = r.Select(u => u.Message.Sender.User.Name).FirstOrDefault(),
        //        OponentId = r.Select(u => u.Message.Sender.Id).FirstOrDefault(),
        //        DialogName = r.Select(u => u.Message.Conversation.Name).FirstOrDefault(),
        //        Allmsgs = CTX.ChatMsgRecipients.Where(rn => rn.Recieved == null && rn.Recipient.Id == UserId).Count()
        //    });

        //    return Json(Conversation, JsonRequestBehavior.AllowGet);

        //}
        //public JsonResult ConversationSearch(int toUserId, string mac)
        //{
        //    CoreContext CTX = new CoreContext();

        //    var fromUserIdUserTbl = PersonalizationService.GetCurrentUserInfo(mac).User.Id;
        //    var fromUserId = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == fromUserIdUserTbl).Id;


        //    var Conversation = CTX.Conversations.SingleOrDefault(c => c.Participants.Any(p => p.SUser.Id == toUserId) && c.Participants.Any(p => p.SUser.Id == fromUserId) && c.Participants.Count() == 2 && c.Code == null);

        //    if (Conversation == null)
        //    {
        //        Guid forNewConversation = Guid.NewGuid();

        //        var toUser = CTX.SocialUsers.Single(u => u.Id == toUserId);
        //        var fromUser = CTX.SocialUsers.Single(u => u.Id == fromUserId);

        //        var NewConversation = new Conversation { Created = DateTimeOffset.UtcNow};

        //        var ParticipantOne = new ChatParticipant { Connect = DateTimeOffset.UtcNow, Conversation = NewConversation, SUser = toUser };
        //        var ParticipantTwo = new ChatParticipant { Connect = DateTimeOffset.UtcNow, Conversation = NewConversation, SUser = fromUser };

        //        CTX.Conversations.Add(NewConversation);
        //        CTX.ChatParticipants.Add(ParticipantOne);
        //        CTX.ChatParticipants.Add(ParticipantTwo);
        //        CTX.SaveChanges();

        //        return Json(NewConversation.Id, JsonRequestBehavior.AllowGet);
        //    }
        //    var ConversationId = Conversation.Id;

            
            

        //    return Json(ConversationId, JsonRequestBehavior.AllowGet);


        //}

        //public JsonResult Readed(string mac, int ConversationId)
        //{
        //    CoreContext CTX = new CoreContext();

        //    var MainUserId = PersonalizationService.GetCurrentUserInfo(mac).User.Id;
        //    var UserId = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == MainUserId).Id;

        //    var MsgToRead = CTX.ChatMsgRecipients.Where(mr => mr.Message.Conversation.Id == ConversationId && mr.Recipient.Id == UserId && mr.Recieved == null);

        //    foreach (var msgs in MsgToRead)
        //    {
        //        msgs.Recieved = DateTimeOffset.UtcNow;
        //    }

        //    CTX.SaveChanges();

        //    return Json("Read", JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult Dialogs(int UserId)
        //{
            
        //    CoreContext CTX = new CoreContext();

        //    var dialogs = CTX.ChatMsgRecipients.Where(mr => mr.Recieved == null && mr.Recipient.Id == UserId).GroupBy(mr => mr.Message.Conversation.Id).Select(mr => new 
        //    {
        //        Name = mr.Key,
        //        Count = mr.Count(),
        //        DATA = mr.Select(x => new
        //        {
        //            OponentName = x.Message.Sender.User.Name,
        //            OponentId = x.Message.Sender.Id,
        //            ConversationId = x.Message.Conversation.Id,
        //            ConversationName = x.Message.Conversation.Code
        //        })
        //    });

        //    return Json(dialogs, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult UniqueDialogCount(int UserId)
        //{
        //    CoreContext CTX = new CoreContext();

        //    var Dialogs = CTX.Conversations.Where(c => c.Messages.Any(m => m.Recipients.Any(r => r.Recipient.Id == UserId && r.Recieved == null))).Count();

        //    return Json(Dialogs, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult People(string mac, int oponentId)
        //{
        //    CoreContext CTX = new CoreContext();
        //    var UserId = PersonalizationService.GetCurrentUserInfo(mac).User.Id;
        //    string friendStatus = "default";
        //    var friend = CTX.Friends.Any(f => f.Confirme != null && ((f.ConfirmUser.Id == UserId && f.RequestUser.Id == oponentId) || (f.RequestUser.Id == UserId && f.ConfirmUser.Id == oponentId)));
        //    if (friend)
        //    {
        //        friendStatus = "friend";
        //    }
        //    else
        //    {
        //        var addconf = CTX.Friends.Any(f => (f.ConfirmUser.Id == UserId && f.RequestUser.Id == oponentId) || (f.RequestUser.Id == UserId && f.ConfirmUser.Id == oponentId));
        //        if (!addconf)
        //        {
        //            friendStatus = "user";
        //        }
        //        else
        //        {
        //            var request = CTX.Friends.Any(f => f.ConfirmUser.Id == UserId && f.RequestUser.Id == oponentId);
        //            if (request)
        //            {
        //                friendStatus = "request";
        //            }
        //            else
        //            {
        //                friendStatus = "confirm";
        //            }
        //        }
        //    }

        //    var People = CTX.SocialUsers.Where(su => su.Id == oponentId).Select(su => new
        //    {
        //        UserId = su.User.Id,
        //        SocialUserId = su.Id,
        //        Name = su.User.Name,
        //        Phone = su.User.Phone,
        //        isShown = su.isShown,
        //        LastActivity = su.LastActivityDate,
        //        Connection = su.Connections.Select(c => c.ConnectionId),
        //        status = friendStatus
        //    }).SingleOrDefault();

        //    return Json(People, JsonRequestBehavior.AllowGet);
        //}

    }
}
