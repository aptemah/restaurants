using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Intouch.Core;

namespace Intouch.SocialNetwork.Hubs
{
    //[CORSActionFilter]
    public class RestHub : Hub
    {
        protected readonly CoreContext _db = new CoreContext();
        public void Connect(Guid sessionId)
        {
            var session = _db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null)
            {
                Clients.Caller.onNoSession();
                return;
            }
            var user = session.RestAppUser;
            var role = user.Role;
            var connection = Context.ConnectionId;

            var socialUserExist = _db.SocialUsers.Any(su => su.RestUser.Id == user.Id);
            if (!socialUserExist)
            {
                var newSocialUser = new SocialUser { RestUser = user, LastActivityDate = DateTimeOffset.UtcNow };
                var newConnection = new Connections
                {
                    SUser = newSocialUser,
                    ConnectionId = connection,
                    ConnectionTime = DateTimeOffset.UtcNow
                };
                _db.SocialUsers.Add(newSocialUser);
                _db.Connections.Add(newConnection);
                _db.SaveChanges();
            }
            else
            {
                var socialUser = _db.SocialUsers.Single(su => su.RestUser.Id == user.Id);
                var oldConnections = _db.Connections.Where(c => c.SUser.Id == socialUser.Id);
                foreach (var toOldConnection in oldConnections)
                {
                    if (toOldConnection.ConnectionTime < DateTimeOffset.Now.AddDays(-1))
                    {
                        _db.Connections.Remove(toOldConnection);
                    }
                }

                var newConnection = new Connections
                {
                    SUser = socialUser,
                    ConnectionId = connection,
                    ConnectionTime = DateTimeOffset.UtcNow
                };

                _db.Connections.Add(newConnection);
                socialUser.LastActivityDate = DateTimeOffset.UtcNow;
                _db.SaveChanges();
            }
        }
        public void ConnectToConversation(Guid sessionId, int pointId)
        {
            var session = _db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null)
            {
                Clients.Caller.onNoSession();
                return;
            }
            var user = session.RestAppUser;

            var socialUser = _db.SocialUsers.Single(su => su.RestUser.Id == user.Id);
            var conversation = _db.Conversations.Single(c => c.Code == "Restaurant" && c.Point.Id == pointId);
            var checkParticipants =
                _db.ChatParticipants.Where(c => c.Conversation.Code == "Restaurant" && c.SUser.Id == socialUser.Id).ToList();
            foreach (var part in checkParticipants)
            {
                _db.ChatParticipants.Remove(part);
            }
            _db.SaveChanges();

            var newPart = new ChatParticipant()
            {
                Connect = DateTimeOffset.UtcNow,
                Conversation = conversation,
                SUser = socialUser
            };
            _db.ChatParticipants.Add(newPart);
            _db.SaveChanges();

        }
        public void UsersInChat(Guid sessionId, int point)
        {
            var session = _db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null)
            {
                Clients.Caller.onNoSession();
                return;
            }
            var user = session.RestAppUser;

            if (point != 0)
            {
                var network = _db.Points.Single(p => p.Id == point).Network.Id;
                var socialUserData = _db.SocialUsers.Single(su => su.RestUser.Id == user.Id);
                var usersInClub =
                    _db.SocialUsers.Where(u => u.Participants.Any(p => p.Conversation.Point.Id == point) &&
                                               u.RestUser.Name != null //&&
                    // не показывать тех у кого нет имени
                    //u.RestUser.Role == RestRole.User)
                                               )
                        .Select(p => new
                        {
                            RestUserId = p.RestUser.Id,
                            SocialUserId = p.Id,
                            UsersName = p.RestUser.Name,
                            LastActivity = p.LastActivityDate,
                            UsersPhoto = p.RestUser.Photo,
                            Connection = p.Connections.Select(c => c.ConnectionId)
                        });
                Clients.Caller.onConnected(usersInClub);
            }
            else
            {
                Clients.Caller.onConnected();
            }
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var connection = Context.ConnectionId;
            var connectionString = _db.Connections.SingleOrDefault(c => c.ConnectionId == connection);

            var user = connectionString.SUser;
            var participant = _db.ChatParticipants.Single(p => p.SUser.Id == user.Id);

            _db.Connections.Remove(connectionString);
            _db.ChatParticipants.Remove(participant);
            _db.SaveChanges();

            Clients.All.onUserDisconnected();

            return base.OnDisconnected(stopCalled);
        }
        public void GetMessages(Guid SessionId, int PointId)
        {
            var session = _db.RestAppSessions.SingleOrDefault(s => s.Id == SessionId);
            if (session == null)
            {
                Clients.Caller.onNoSession();
                return;
            }
            var user = session.RestAppUser;
            var recipients = _db.SocialUsers.Where(s => s.Participants.Any(a => a.Conversation.Point.Id == PointId)).SelectMany(s => s.Connections);
            var allMsgs =
                _db.ChatMessages.Where(m => m.Conversation.Point.Id == PointId).Select(s => new
                {
                    Id = s.Id,
                    Messages = s.Messages,
                    TypeMessage = s.TypeMessage,
                    SenderName = s.Sender.RestUser.Name,
                    SenderPhoto = s.Sender.RestUser.Photo,
                    Date = s.Created,
                    Status = (s.Sender.RestUser.Id == user.Id ) ? ("my") : (""),
                    Min = s.Created.Minute,
                    Hour = s.Created.Hour,
                    Day = s.Created.Day,
                    Month = s.Created.Month,
                    Year = s.Created.Year,

                }).OrderBy(o => o.Date);
            Clients.Caller.onMessage(allMsgs);
        }
        public void SendMessage(string Message, int TypeMsg, Guid SessionId, int PointId)
        {
            var session = _db.RestAppSessions.SingleOrDefault(s => s.Id == SessionId && s.EndDate == null);
            if (session == null)
            {
                Clients.Caller.onNoSession();
                return;
            }
            var sender = session.RestAppUser;
            var recipients = _db.SocialUsers.Where(s => s.Participants.Any(a => a.Conversation.Point.Id == PointId)).SelectMany(s => s.Connections).Include(s => s.SUser);

            var conversation = _db.Conversations.Single(c => c.Point.Id == PointId);

            var senders = _db.SocialUsers.Single(u => u.RestUser.Id == sender.Id);

            var date = DateTimeOffset.Now;

            var newMsg = new ChatMessage
            {
                Conversation = conversation,
                Created = date,
                TypeMessage = (TypeMessage)TypeMsg,
                Messages = Message,
                Sender = senders
            };

            _db.ChatMessages.Add(newMsg);
            _db.SaveChanges();

            foreach (var item in recipients)
            {
                Clients.Client(item.ConnectionId)
                    .sendMessages(
                        new
                        {
                            Id = newMsg.Id,
                            Messages = newMsg.Messages,
                            TypeMessage = newMsg.TypeMessage,
                            SenderName = newMsg.Sender.RestUser.Name,
                            SenderPhoto = newMsg.Sender.RestUser.Photo,
                            Date = newMsg.Created,
                            Status = (newMsg.Sender.Id == item.SUser.Id) ? ("my") : (""),
                            Min = newMsg.Created.Minute,
                            Hour = newMsg.Created.Hour,
                            Day = newMsg.Created.Day,
                            Month = newMsg.Created.Month,
                            Year = newMsg.Created.Year,
                        });
            }
            //Clients.Caller.sendMessagesToCaller(
            //        new
            //        {
            //            Id = newMsg.Id,
            //            Messages = newMsg.Messages,
            //            TypeMessage = newMsg.TypeMessage,
            //            SenderName = newMsg.Sender.RestUser.Name,
            //            SenderPhoto = newMsg.Sender.RestUser.Photo,
            //            Date = newMsg.Created,
            //            Status = (newMsg.Sender.RestUser.Id == sender.Id) ? ("my") : (""),
            //            Min = newMsg.Created.Minute,
            //            Hour = newMsg.Created.Hour,
            //            Day = newMsg.Created.Day,
            //            Month = newMsg.Created.Month,
            //            Year = newMsg.Created.Year,
            //        });
        }
        public void OrderPartForOfficiant(int OrderPartId)
        {
            var order = _db.RestOrders.SingleOrDefault(o => o.RestOrderParts.Any(p => p.Id == OrderPartId));
            var officiant = _db.RestAppUsers.SingleOrDefault(o => o.Id == order.Officiant.Id);
            var connection = _db.Connections.SingleOrDefault(c => c.SUser.Id == officiant.SocialUser.Id);
            Clients.Client(connection.ConnectionId).onNewOrderPart(OrderPartId);
        }
        public void OrderPartForManagers(int OrderPartId)
        {
            var order = _db.RestOrders.SingleOrDefault(o => o.RestOrderParts.Any(p => p.Id == OrderPartId));
            var managers = _db.RestAppUsers.Where(o => o.Point.Id == order.RestPoint.Point.Id);
            foreach (var i in managers)
            {
                var connection = _db.Connections.SingleOrDefault(c => c.SUser.Id == i.SocialUser.Id);
                Clients.Client(connection.ConnectionId).onNewOrderPart(OrderPartId);
            }
        }
        public void TestMethod(string test)
        {
            var rand = new Random();
            var code = rand.Next(1111, 9999);
            Clients.Caller.onTest(new { Code = code, Test = test, connect = Context.ConnectionId });
        }
    }
}