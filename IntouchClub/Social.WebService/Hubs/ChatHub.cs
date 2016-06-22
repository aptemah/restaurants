//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Intouch.Core;
//using Microsoft.AspNet.SignalR;
//using System.Threading;
//using System.Web.Mvc;


//namespace Intouch.Social.Hubs
//{
//    public class ChatHub : Hub
//    {
        
//        CoreContext CTX = new CoreContext();
        
//        public void Connect(Guid UserId, Guid? ClubId)
//        {
            

//            var id = Context.ConnectionId;
//            var userName = CTX.Users.SingleOrDefault(u => u.Id == UserId).Name;
//            //var tableNo = CTX.KarUsers.SingleOrDefault(u => u.Id == UserId).TableNo;
//            Guid ConversId;

//            //Object ConnectedUsers;
//            Object CurrentMessage;

//            IEnumerable<object> ConnectedUsers;

//            if (ClubId.HasValue)
//            {
//                var code = ClubId + "/main";

//                ConversId = CTX.Conversations.Single(c => c.Code == code).Id;

//                CurrentMessage = CTX.Conversations.Single(c => c.Code == code).Messages.Select(m => new
//                {
//                    Id = m.Id,
//                    Message = m.Messages,
//                    SenderId = m.Sender.Id,
//                    SenderName = m.Sender.User.Name,
//                    Created = m.Created
//                }).OrderBy(m => m.Created);

//                ConnectedUsers = CTX.Conversations.Single(c => c.Code == code).Participants.Select(c => new
//                {
//                    Id = c.SUser.Id,
//                    Name = c.SUser.User.Name,
//                    //TableNo = c.KarUser.TableNo,
//                    ConnectionId = c.SUser.Connections.Select(cc => cc.ConnectionId),
//                    ConnectTime = c.Connect,
                    
//                });//.OrderBy(c => c.TableNo);
//            }
//            else
//            {
//                ConversId = CTX.Conversations.Single(c => c.Code == "main").Id;

//                CurrentMessage = CTX.Conversations.Single(c => c.Id == ConversId).Messages.Select(m => new
//                {
//                    Id = m.Id,
//                    Message = m.Messages,
//                    SenderId = m.Sender.Id,
//                    SenderName = m.Sender.User.Name,
//                    Created = m.Created,
//                }).OrderBy(m => m.Created);

//                ConnectedUsers = CTX.Conversations.Single(c => c.Id == ConversId).Participants.Select(c => new
//                {
//                    Id = c.SUser.User.Id,
//                    Name = c.SUser.User.Name,
//                   // TableNo = c.KarUser.User.TableNo,
//                    ConnectionId = c.SUser.Connections.Select(cc => cc.ConnectionId),
//                    ConnectTime = c.Connect,
                    
//                });//.OrderBy(c => c.TableNo);



//            }
    


//            if (CTX.ChatParticipants.Where(p => p.User.User.Id == UserId && p.Conversation.Id == ConversId).Count() != 1)
//                {
//                    var User = CTX.SocialUsers.FirstOrDefault(u => u.Id == UserId);
//                    var Conversation = CTX.Conversations.SingleOrDefault(c => c.Id == ConversId);
//                    //ПОдключение юзера 1 если он не подключен
//                    CTX.ChatParticipants.Add(new ChatParticipant { Id = Guid.NewGuid(), ConnectionId = id, User = User, Connect = DateTimeOffset.UtcNow, Conversation = Conversation });
//                    CTX.SaveChanges();
                    
//                    // send to caller
//                    //Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

//                    // send to all except caller client
//                    Clients.AllExcept(id).onNewUserConnected(id, userName, UserId); // TODO: tableNo убрал. Надо убрать и из js файла. 
//                    Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);
//                }
//                else
//                {
//                    //var ConnectionId = CTX.Connections.SingleOrDefault(c => c.User.Id == UserId).ConnectionId;
//                    // send to caller
//                    //var User = CTX.SocialUsers.FirstOrDefault(u => u.Id == UserId);

//                    //var Conversation = CTX.Conversations.SingleOrDefault(c => c.Id == ConversId);

//                    ChatParticipant userChangeConnectionId = CTX.ChatParticipants.Single(p => p.Conversation.Id == ConversId && p.User.Id == UserId); // TODO: Решить ид какой таблицы использовать в социалке. пока используется таблица от караоке

//                    userChangeConnectionId.ConnectionId = id;
//                    CTX.SaveChanges();
                    
                    
//                    Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);
//                    Clients.AllExcept(id).onNewUserConnected(id, userName, UserId); // todo: Убрал tableNo разобраттся с жс
//                    //Clients.AllExcept(id).onUserReConnected(id, UserId);

//                    // send to all except caller client
//                    //Clients.AllExcept(id).onNewUserConnected(ConnectionId, userName, tableNo, UserId);
//                }
//        }

//        public void ConnectToPersonalConversation(Guid ConversationId)
//        {
//            var Participants = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversationId).Select(p => new 
//            {
//                Id = p.User.Id,
//                Name = p.User.User.Name,
//                //TableNo = p.KarUser.TableNo,
//                ConnectionId = p.ConnectionId,
//                ConnectTime = p.Connect,
//                // TODO: status online / offline
//            }).OrderBy(p => p.Id);
//            var Messages = CTX.ChatMessages.Where(m => m.Conversation.Id == ConversationId).Select(m => new
//            {
//                Id = m.Id,
//                Message = m.Messages,
//                SenderId = m.Sender.Id,
//                SenderName = m.Sender.User.Name,
//                Created = m.Created,

//            }).OrderBy(m => m.Created);

//            Clients.Caller.onConnectedToConversation(Participants, Messages);
//        }

//        public void SendMsgToPrivateConversation(Guid ConversationId, string message, Guid fromUserId)
//        {
//            var FromUserConnectionId = Context.ConnectionId;

//            var toUserIdId = CTX.Conversations.Single(c => c.Id == ConversationId).Participants.Single(p => p.User.Id != fromUserId).User.Id;

//            var toUserId = CTX.Conversations.Single(c => c.Code == "main").Participants.Single(p => p.User.Id == toUserIdId).ConnectionId;

//            var userName = CTX.Conversations.Single(c => c.Id == ConversationId).Participants.Single(p => p.User.Id == fromUserId).User.User.Name;
//            var Conversation = CTX.Conversations.Single(c => c.Id == ConversationId);
//            var Sender = CTX.SocialUsers.Single(u => u.Id == fromUserId);
//            var Recipient = CTX.Conversations.Single(c => c.Id == ConversationId).Participants.Single(p => p.User.Id != fromUserId).User;

//            var NewMsg = new ChatMessage { Id = Guid.NewGuid(), Conversation = Conversation, Created = DateTimeOffset.UtcNow, Messages = message, Sender = Sender };
//            var NewRcpt = new ChatMsgRecipient { Id = Guid.NewGuid(), Message = NewMsg, Recipient = Recipient};
            
//            CTX.ChatMessages.Add(NewMsg);
//            CTX.ChatMsgRecipients.Add(NewRcpt);
//            CTX.SaveChanges();

//            var messageId = NewMsg.Id;

//            Clients.Client(toUserId).sendPrivateMessage(ConversationId, userName, message, messageId);

//            Clients.Caller.sendPrivateMessage(ConversationId, userName, message);
//        }


//        public void SendMessageToAll(string message, Guid UserId, Guid ClubId)
//        {
//            // store last 100 messages in cache
//            AddMessageinCache(UserId, ClubId, message);

//            var userName = CTX.KarUsers.SingleOrDefault(u => u.Id == UserId).User.Name;
//            var date = CTX.ChatMessages.Where(m => m.Sender.Id == UserId).OrderBy(m => m.Created).FirstOrDefault().Created;
//            var messageID = CTX.ChatMessages.Where(m => m.Sender.Id == UserId).OrderBy(m => m.Created).FirstOrDefault().Id;
//            // Broad cast message
//            Clients.All.messageReceived(userName, message, date, messageID);
//        }

//        private void AddMessageinCache(Guid UserId, Guid ClubId, string message)
//        {
//            var code = ClubId + "/main";
//            Guid ConversId = CTX.Conversations.Single(c => c.Code == code).Id;
//            var Conversation = CTX.Conversations.Single(c => c.Code == code);
//            var Sender = CTX.SocialUsers.FirstOrDefault(u => u.Id == UserId);

//            var Message = new ChatMessage 
//            {
//                Id = Guid.NewGuid(),
//                Created = DateTimeOffset.UtcNow,
//                Conversation = Conversation,
//                Messages = message,
//                Sender = Sender
//            };
//            CTX.ChatMessages.Add(Message);

//            var Participants = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversId);

//            foreach (var i in Participants)
//            {
//                if (i.User.Id != UserId)
//                {
//                    var Recipient = CTX.SocialUsers.SingleOrDefault(u => u.Id == i.User.Id);
//                    var Recipients = new ChatMsgRecipient
//                    {
//                        Id = Guid.NewGuid(),
//                        Recipient = Recipient,
//                        Message = Message
//                    };
//                    CTX.ChatMsgRecipients.Add(Recipients);
//                }
//            }

//            CTX.SaveChanges();
//        }
        

//        public void CountForMessage(Guid UserId, Guid ConversationId)
//        {
//            // всем если сообщение из общего чата
//            // и тем кто в группе если не общий чат

//            Thread.Sleep(100);

//            var Oponent = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversationId && p.SUser.Id != UserId).SingleOrDefault();

//            var AllMsgsCount = CTX.ChatMsgRecipients.Where(r => r.Recieved == null && r.Recipient.Id == Oponent.SUser.Id).Count(); //TODO: проверить на сервере. нужен ли нам +1

//            var toUserId = CTX.ChatParticipants.Where(p => p.Conversation.Code == "main" && p.SUser.Id == Oponent.SUser.Id).Select(p => p.SUser.Connections.Select(c => c.ConnectionId)).SingleOrDefault().ToString();

//            var Dialogs = CTX.Conversations.Where(c => c.Messages.Any(m => m.Recipients.Any(r => r.Recipient.Id == Oponent.SUser.Id && r.Recieved == null))).Count();

//            var UserDialogs = CTX.ChatMsgRecipients.Where(mr => mr.Recieved == null && mr.Recipient.Id == Oponent.SUser.Id && mr.Message.Sender.Id == UserId).GroupBy(mr => mr.Message.Conversation.Id).Select(mr => new 
//            {
//                Name = mr.Key,
//                Count = mr.Count(),
//                DATA = mr.Select(x => new
//                {
//                    OponentName = x.Message.Sender.User.Name,
//                    OponentId = x.Message.Sender.Id,
//                    ConversationId = x.Message.Conversation.Id,
//                    ConversationName = x.Message.Conversation.Code
//                }),
//                date = mr.Select(xx => xx.Message.Created).FirstOrDefault()
//            }).OrderBy(o => o.date);

//            Clients.Client(toUserId).countMessage(AllMsgsCount, Dialogs, UserDialogs);
//        }


//        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
//        {
//            var item = CTX.ChatParticipants.FirstOrDefault(x => x.SUser.Connections.Select(c => c.ConnectionId) == Context.ConnectionId);
//            if (item != null)
//            {
//                var UserId = item.SUser.Id;
//                CTX.ChatParticipants.Remove(item);
//                CTX.SaveChanges();

//                var id = item.Id;
                
//                Clients.All.onUserDisconnected(id, UserId);

//            }

//            return base.OnDisconnected(stopCalled);
//        }
//    }
//}