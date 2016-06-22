using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intouch.Core;
using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Web.Mvc;
using Core.ApplicationServices;


namespace ChatWithDb.Hubs
{
    public class MainHub : Hub
    {
        CoreContext CTX = new CoreContext();

        //public void Connect(string mac)
        //{
        //    //Пока система работает только для людей в клубе. Для аутклаб пока не прописано
        //    var PointID = PersonalizationService.GetCurrentUserInfo(mac).Point.Id; // Id точки по маку
        //    var UserID = PersonalizationService.GetCurrentUserInfo(mac).User.Id; // Id пользователя по маку
        //    var DeviceID = PersonalizationService.GetCurrentUserInfo(mac).Device.Id; // Id Устройства по маку

        //    var User = CTX.Users.SingleOrDefault(u => u.Id == UserID); // Сущность пользователя
        //    var Device = CTX.Devices.SingleOrDefault(d => d.Id == DeviceID); // Сущность устройства

        //    var connection = Context.ConnectionId; // Id Соединения

        //    var WiFiSessionsInPoint = CTX.WiFiSessions.Where(wfs => wfs.Point.Id == PointID); // Вай фай сессии в определенной точке
        //    var WiFiSessionsWithUsers = WiFiSessionsInPoint.Where(wfsip => wfsip.Device.UserDeviceSessions.Any(uds => uds.User.Id != null)); // Вай фай сессии в которых есть привязка к пользователю
        //    var WiFiSessionsWithSocialUsers = WiFiSessionsWithUsers.Where(f => f.Device.UserDeviceSessions.FirstOrDefault().User.SocialUsers.Select(su => su.Id).FirstOrDefault() != 0);
        //    // Уникальные пользователи в клубе с информацией (очень странный код написал :) ) тут еще ошибка с тем что выводит даж тех людей которых нету в таблице SocialUsers


           
        //    //Проверяем есть ли такой пользователь в таблице социальной сети

        //    var SocialUserExist = CTX.SocialUsers.Any(su => su.User.Id == UserID);
        //    if (!SocialUserExist)
        //    {
        //        var NewSocialUser = new SocialUser { User = User, LastActivityDate = DateTimeOffset.UtcNow };
        //        var NewConnection = new Connections { SUser = NewSocialUser, ConnectionId = connection };

        //        CTX.SocialUsers.Add(NewSocialUser);
        //        CTX.Connections.Add(NewConnection);

        //        CTX.SaveChanges();

        //        var newUserInfo = CTX.SocialUsers.Where(su => su.Id == NewSocialUser.Id).Select(su => new
        //        {
        //            UserId = su.User.Id,
        //            SocialUserId = su.Id,
        //            Name = su.User.Name,
        //            Phone = su.User.Phone,
        //            isShown = su.isShown,
        //            LastActivity = su.LastActivityDate,
        //            Connection = su.Connections.Select(c => c.ConnectionId)
        //        }).SingleOrDefault();
        //        Clients.AllExcept(connection).onNewUserConnected(newUserInfo); // показывается всем кроме зашедшего в комнату клуба. Для совсем нового пользователя которого раньше небыло в сети
        //    }
        //    else
        //    {
        //        // если юзер есть записываем в connection(таблицу): Id этого соединения и дату! потому как при событии Disconnect это удаляется!
                
        //        var SocialUser = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == UserID);
        //        var OldConnection = CTX.Connections.SingleOrDefault(c => c.SUser.Id == UserID);
        //        if (OldConnection != null)
        //        {
        //            CTX.Connections.Remove(OldConnection);
        //        }

        //        var NewConnection = new Connections {SUser = SocialUser, ConnectionId = connection};
        //        CTX.Connections.Add(NewConnection);
        //        SocialUser.LastActivityDate = DateTimeOffset.UtcNow;

                
        //        CTX.SaveChanges();

        //        var userInfoForClient = CTX.SocialUsers.Where(su => su.User.Id == UserID).Select(su => new
        //        {
        //            UserId = su.User.Id,
        //            SocialUserId = su.Id,
        //            Name = su.User.Name,
        //            Phone = su.User.Phone,
        //            isShown = su.isShown,
        //            LastActivity = su.LastActivityDate,
        //            Connection = su.Connections.Select(c => c.ConnectionId)
        //        }).SingleOrDefault();

        //        Clients.AllExcept(connection).onOldUserConnected(userInfoForClient); // показывается всем кроме зашедшего в комнату клуба. Для пользователя который уже бывал в клубе(изменить ему статус с оффлайн на онлайн)
        //    }

        //    //var UsersInClub = WiFiSessionsWithSocialUsers.GroupBy(wfs => wfs.Device.UserDeviceSessions.FirstOrDefault().User.Id).Select(wfs => new
        //    //{
        //    //    mac = wfs.Select(m => m.Device.Mac).FirstOrDefault(),
        //    //    Users = wfs.Key,
        //    //    online = wfs.Any(a => a.Closed == null),
        //    //    lastActivityDate = wfs.Max(m => m.Closed),
        //    //    Name = wfs.Select(f => f.Device.UserDeviceSessions.FirstOrDefault().User.Name).FirstOrDefault(),
        //    //    SocialUserId = wfs.Select(f => f.Device.UserDeviceSessions.FirstOrDefault().User.SocialUsers.Select(su => su.Id).FirstOrDefault()).FirstOrDefault(),
        //    //});
        //    //понять точные параметры дял передачи
        //    //те кто бывал в клубе
        //    var UsersInClub =
        //        CTX.SocialUsers.Where(
        //            su =>
        //                su.User.UserDeviceSessions.Any(
        //                    uds => uds.Device.WifiSessions.Any(wfs => wfs.Point.Id == PointID))).Select(su => new
        //                    {
        //                        mac = su.User.UserDeviceSessions.Select(uds => uds.Device.Mac),  // Проблема в том что в будущем маков будет больш чем 1 если человек сразу с 2х устройств онлайн
        //                        UserId = su.User.Id,
        //                        SocialUserId = su.Id,
        //                        Name = su.User.Name,
        //                        Phone = su.User.Phone,
        //                        isShown = su.isShown,
        //                        LastActivity = su.LastActivityDate,
        //                        Connection = su.Connections.Select(c => c.ConnectionId),
        //                        online = su.User.UserDeviceSessions.Any(
        //                                        uds => uds.Device.WifiSessions.Any(wfs => wfs.Point.Id == PointID && wfs.Closed == null))
        //                    });
        //    // Все друзья которые есть не зависимо от клуба.
        //    //var friends = CTX.SocialUsers.Where(su => su.RequestFriends.Any(rf => rf.RequestUser.Id == User.Id && rf.Confirme != null) || su.ConfirmFriends.Any(cf => cf.ConfirmUser.Id == User.Id && cf.Confirme != null)).Select(su => new
        //    //{
        //    //    mac = su.User.UserDeviceSessions.Select(uds => uds.Device.Mac),  // Проблема в том что в будущем маков будет больш чем 1 если человек сразу с 2х устройств онлайн
        //    //    UserId = su.User.Id,
        //    //    SocialUserId = su.Id,
        //    //    Name = su.User.Name,
        //    //    Phone = su.User.Phone,
        //    //    isShown = su.isShown,
        //    //    LastActivity = su.LastActivityDate,
        //    //    Connection = su.Connections.Select(c => c.ConnectionId),
        //    //    online = su.User.UserDeviceSessions.Any(
        //    //                    uds => uds.Device.WifiSessions.Any(wfs => wfs.Point.Id == PointID && wfs.Closed == null))
        //    //});

        //    var friends = CTX.Friends.Where(f => f.Confirme != null && (f.RequestUser.Id == User.Id || f.ConfirmUser.Id == User.Id)).Select(f => new
        //    {
        //        //mac = f.RequestUser.Id != User.Id ? f.RequestUser.User.UserDeviceSessions.Select(uds => uds.Device.Mac) : f.ConfirmUser.User.UserDeviceSessions.Select(uds => uds.Device.Mac),  // Проблема в том что в будущем маков будет больш чем 1 если человек сразу с 2х устройств онлайн
        //        UserId = f.RequestUser.Id != User.Id ? f.RequestUser.User.Id : f.ConfirmUser.User.Id,
        //        SocialUserId = f.RequestUser.Id != User.Id ? f.RequestUser.Id : f.ConfirmUser.Id,
        //        Name = f.RequestUser.Id != User.Id ? f.RequestUser.User.Name : f.ConfirmUser.User.Name,
        //        Phone = f.RequestUser.Id != User.Id ? f.RequestUser.User.Phone : f.ConfirmUser.User.Phone,
        //        isShown = f.RequestUser.Id != User.Id ? f.RequestUser.isShown : f.ConfirmUser.isShown,
        //        LastActivity = f.RequestUser.Id != User.Id ? f.RequestUser.LastActivityDate : f.ConfirmUser.LastActivityDate,
        //        //Connection = f.RequestUser.Id != User.Id ? f.RequestUser.Connections.Select(c => c.ConnectionId) : f.ConfirmUser.Connections.Select(c => c.ConnectionId) 
        //    });

        //    //var friends2 = CTX.ChatFriends

        //    //var friendsRequest2 =
        //    //    CTX.SocialUsers.Where(
        //    //        su => su.ConfirmFriends.Any(cf => cf.ConfirmUser.Id == User.Id && cf.Confirme == null)).Select(su => new
        //    //        {
        //    //            mac = su.User.UserDeviceSessions.Select(uds => uds.Device.Mac),  // Проблема в том что в будущем маков будет больш чем 1 если человек сразу с 2х устройств онлайн
        //    //            UserId = su.User.Id,
        //    //            SocialUserId = su.Id,
        //    //            Name = su.User.Name,
        //    //            Phone = su.User.Phone,
        //    //            isShown = su.isShown,
        //    //            LastActivity = su.LastActivityDate,
        //    //            Connection = su.Connections.Select(c => c.ConnectionId),
        //    //            online = su.User.UserDeviceSessions.Any(
        //    //                            uds => uds.Device.WifiSessions.Any(wfs => wfs.Point.Id == PointID && wfs.Closed == null))
        //    //        }); ;

        //    var friendsRequest = CTX.Friends.Where(f => f.ConfirmUser.Id == User.Id && f.Confirme == null).Select(su => new
        //    {
        //        mac = su.RequestUser.User.UserDeviceSessions.Select(uds => uds.Device.Mac),  // Проблема в том что в будущем маков будет больш чем 1 если человек сразу с 2х устройств онлайн
        //        UserId = su.RequestUser.User.Id,
        //        SocialUserId = su.RequestUser.Id,
        //        Name = su.RequestUser.User.Name,
        //        Phone = su.RequestUser.User.Phone,
        //        isShown = su.RequestUser.isShown,
        //        LastActivity = su.RequestUser.LastActivityDate,
        //        Connection = su.RequestUser.Connections.Select(c => c.ConnectionId),
        //        online = su.RequestUser.User.UserDeviceSessions.Any(
        //                        uds => uds.Device.WifiSessions.Any(wfs => wfs.Point.Id == PointID && wfs.Closed == null))
        //    }); ;
        //    Clients.Caller.onConnected(UsersInClub, friends, friendsRequest); // показывается зашедшему в комнату клуба
        //}

        //// Подключение к персональному чату
        //public void ConnectToPersonalConversation(int ConversationId, string mac)
        //{
        //    var Device = PersonalizationService.GetCurrentUserInfo(mac).Device;
        //    var User = PersonalizationService.GetCurrentUserInfo(mac).User;
        //    var SocUser = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == User.Id);
        //    var Participants = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversationId).Select(p => new
        //    {
        //        Id = p.SUser.Id,
        //        Name = p.SUser.User.Name,
        //        //ConnectionId = p.SUser.Connections.SingleOrDefault(c => c.Device.Id == Device.Id) == null ? null : p.SUser.Connections.SingleOrDefault(c => c.Device.Id == Device.Id).ConnectionId, 
        //        ConnectTime = p.Connect,
        //        // TODO: status online / offline
        //    }).OrderBy(p => p.Id);

        //    var Messages = CTX.ChatMessages.Where(m => m.Conversation.Id == ConversationId).Select(m => new
        //    {
        //        Id = m.Id,
        //        Message = m.Messages,
        //        SenderId = m.Sender.Id,
        //        SenderName = m.Sender.User.Name,
        //        Created = m.Created,

        //    }).OrderBy(m => m.Created);

        //    var Oponent = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversationId && p.SUser.Id != SocUser.Id).Select(p => new
        //    {
        //        Id = p.SUser.Id,
        //        Name = p.SUser.User.Name,
        //        //ConnectionId = p.SUser.Connections.SingleOrDefault(c => c.Device.Id == Device.Id) == null ? null : p.SUser.Connections.SingleOrDefault(c => c.Device.Id == Device.Id).ConnectionId, 
        //        ConnectTime = p.Connect,
        //        // TODO: status online / offline
        //    }).SingleOrDefault();

        //    Clients.Caller.onConnectedToConversation(Participants, Messages, Oponent);
        //}

        //// Отправка и получение сообщения в персональном чате

        //public void SendMsgToPrivateConversation(int ConversationId, string message, string mac)
        //{
        //    var SenderConnectionId = Context.ConnectionId;
        //    var SenderId = PersonalizationService.GetCurrentUserInfo(mac).User.Id; // SenderId for MainUser
        //    var RecipientId = CTX.Conversations.Single(c => c.Id == ConversationId).Participants.Single(p => p.SUser.User.Id != SenderId).SUser.Id;
        //    var RecipientConnections = CTX.Conversations.Single(c => c.Id == ConversationId).Participants.Single(p => p.SUser.User.Id != SenderId).SUser.Connections;

        //    var userName = CTX.Conversations.Single(c => c.Id == ConversationId).Participants.Single(p => p.SUser.User.Id == SenderId).SUser.User.Name;
        //    var Conversation = CTX.Conversations.Single(c => c.Id == ConversationId);


        //    var Sender = CTX.SocialUsers.Single(u => u.User.Id == SenderId); //Sender for socialUser
        //    var Recipient = CTX.Conversations.Single(c => c.Id == ConversationId).Participants.Single(p => p.SUser.User.Id != SenderId).SUser;

        //    var NewMsg = new ChatMessage {  Conversation = Conversation, Created = DateTimeOffset.UtcNow, Messages = message, Sender = Sender };
        //    var NewRcpt = new ChatMsgRecipient { Message = NewMsg, Recipient = Recipient };

        //    CTX.ChatMessages.Add(NewMsg);
        //    CTX.ChatMsgRecipients.Add(NewRcpt);
        //    CTX.SaveChanges();

        //    var messageId = NewMsg.Id;

        //    foreach(var item in RecipientConnections){
        //        Clients.Client(item.ConnectionId).sendPrivateMessage(ConversationId, userName, message, messageId);
        //    }

        //    Clients.Caller.sendPrivateMessage(ConversationId, userName, message);

        //    //Clients.Clients(connections).sendPrivateMessage(ConversationId, userName, message, messageId);

            
        //}

        //private void AddMessageinCache(string mac, string message, int ConversationId)
        //{
        //    var Sender = PersonalizationService.GetCurrentUserInfo(mac).User;
        //    var Recipients = CTX.Conversations.SingleOrDefault(c => c.Id == ConversationId).Participants.Where(p => p.SUser.User.Id != Sender.Id);


        //    //Guid ConversId = CTX.Conversations.Single(c => c.Code == code).Id;
        //    var Conversation = CTX.Conversations.Single(c => c.Id == ConversationId);
        //    //var Sender = CTX.SocialUsers.FirstOrDefault(u => u.Id == UserId);
        //    var SocialTableSender = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == Sender.Id);

        //    var Message = new ChatMessage
        //    {
        //        Created = DateTimeOffset.UtcNow,
        //        Conversation = Conversation,
        //        Messages = message,
        //        Sender = SocialTableSender
        //    };
        //    CTX.ChatMessages.Add(Message);

        //    foreach (var Recipient in Recipients)
        //    {
                    
        //            var MsgRecipients = new ChatMsgRecipient
        //            {
        //                Recipient = Recipient.SUser,
        //                Message = Message
        //            };
        //            CTX.ChatMsgRecipients.Add(MsgRecipients);
             
        //    }

        //    CTX.SaveChanges();
        //}


        //public void CountForMessage(string mac, int ConversationId)
        //{
        //    Thread.Sleep(100);
        //    var UserId = PersonalizationService.GetCurrentUserInfo(mac).User.Id;
        //    var Oponent = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversationId && p.SUser.User.Id != UserId).SingleOrDefault();
        //    var AllMsgsCount = CTX.ChatMsgRecipients.Where(r => r.Recieved == null && r.Recipient.Id == Oponent.SUser.Id).Count(); //TODO: проверить на сервере. нужен ли нам +1
        //    //var toUserId = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversationId && p.SUser.Id == Oponent.SUser.Id).Select(p => p.SUser.Connections.Select(c => c.ConnectionId)).SingleOrDefault().ToString();
        //    var toUserId = Oponent.SUser.Connections;
        //    var Dialogs = CTX.Conversations.Where(c => c.Messages.Any(m => m.Recipients.Any(r => r.Recipient.Id == Oponent.SUser.Id && r.Recieved == null))).Count();
        //    var UserDialogs = CTX.ChatMsgRecipients.Where(mr => mr.Recieved == null && mr.Recipient.Id == Oponent.SUser.Id && mr.Message.Sender.User.Id == UserId).GroupBy(mr => mr.Message.Conversation.Id).Select(mr => new
        //    {
        //        Name = mr.Key,
        //        Count = mr.Count(),
        //        DATA = mr.Select(x => new
        //        {
        //            OponentName = x.Message.Sender.User.Name,
        //            OponentId = x.Message.Sender.Id,
        //            ConversationId = x.Message.Conversation.Id,
        //            //ConversationName = x.Message.Conversation.Code
        //        }).FirstOrDefault(),
        //        date = mr.Select(xx => xx.Message.Created).FirstOrDefault()
        //    }).OrderBy(o => o.date);

        //    foreach (var connection in toUserId) {
        //        Clients.Client(connection.ConnectionId).countMessage(AllMsgsCount, Dialogs, UserDialogs);    
        //    }
            
        //}

        //public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        //{
        //    var connection = Context.ConnectionId;

        //    var ConnectionString = CTX.Connections.SingleOrDefault(c => c.ConnectionId == connection);
            
        //    CTX.Connections.Remove(ConnectionString);
        //    CTX.SaveChanges();

            
        //    Clients.All.onUserDisconnected(); // отключает пользователя от данной комнаты ( переводит в состояние оффлайн )

        //    return base.OnDisconnected(stopCalled);
        //}

        ///* -- Блок Друзей. Добавление, Удаление, Подтверждение*/
        //// Пока прописаны только caller тоесть уведомление человеку которого добавляют в друзья нету. Так же как нету уведомления человеку которого подтвердили, что он друг.
        //public void friendRequest(string mac, int confirmFriendId)
        //{
        //    var requestFriendUser = PersonalizationService.GetCurrentUserInfo(mac).User;
        //    var requestFriend = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == requestFriendUser.Id);

        //    var confirmFriend = CTX.SocialUsers.Find(confirmFriendId);

        //    var checkFriends = CTX.Friends.SingleOrDefault(f => f.RequestUser.Id == requestFriend.Id && f.ConfirmUser.Id == confirmFriendId);

        //    if (checkFriends == null)
        //    {
        //        var newFriends = new ChatFriend
        //        {
        //            ConfirmUser = confirmFriend,
        //            RequestUser = requestFriend,
        //            Request = DateTimeOffset.UtcNow
        //        };

        //        CTX.Friends.Add(newFriends);
        //        CTX.SaveChanges();
        //        var oponentConnectionIds = confirmFriend.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientFriendRequest();
        //        }


        //        Clients.Caller.onFriendRequest();
        //    }
        //    else
        //    {
        //        var ansvr = "Запрос уже послан на рассмотрение";
        //        Clients.Caller.onExistFriendRequest(ansvr);
        //    }
        //}

        //public void friendConfirm(string mac, int requestedSocialUserId)
        //{
        //    var confirmFriendUser = PersonalizationService.GetCurrentUserInfo(mac).User;
        //    var confirmFriend = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == confirmFriendUser.Id);
        //    var requestUser = CTX.SocialUsers.Find(requestedSocialUserId);

        //    var checkFriends = CTX.Friends.SingleOrDefault(f => f.RequestUser.Id == requestUser.Id && f.ConfirmUser.Id == confirmFriend.Id && f.Confirme == null);
        //    if (checkFriends != null)
        //    {
        //        checkFriends.Confirme = DateTimeOffset.UtcNow;
        //        CTX.SaveChanges();

        //        var oponentConnectionIds = requestUser.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientFriendConfirm();
        //        }

        //        Clients.Caller.onFriendConfirm();
        //    }
        //    else
        //    {
        //        var ansvr = "неясная ошибка. такой друг к вам не добавлялся!";
        //        Clients.Caller.onNotExistFriendConfirm(ansvr);
        //    }
        //}

        //public void friendDiscard(string mac, int deleteSocialUserId)
        //{
        //    var FriendUser = PersonalizationService.GetCurrentUserInfo(mac).User;
        //    var Friend = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == FriendUser.Id);
        //    var requestUser = CTX.SocialUsers.Find(deleteSocialUserId);

        //    var checkFriends = CTX.Friends.SingleOrDefault(f => f.RequestUser.Id == requestUser.Id && f.ConfirmUser.Id == Friend.Id);
        //    if (checkFriends != null)
        //    {
        //        CTX.Friends.Remove(checkFriends);
        //        CTX.SaveChanges();

        //        var oponentConnectionIds = requestUser.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientFriendDiscard();
        //        }

        //        Clients.Caller.onFriendDiscard();
        //    }
        //    else
        //    {
        //        var ansvr = "неясная ошибка. такго друга у вас нету!";
        //        Clients.Caller.onNotExistFriendConfirm(ansvr);
        //    }
        //}

        //public void friendDelete(string mac, int deleteSocialUserId)
        //{
        //    var FriendUser = PersonalizationService.GetCurrentUserInfo(mac).User;
        //    var Friend = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == FriendUser.Id);
        //    var requestUser = CTX.SocialUsers.Find(deleteSocialUserId);

        //    var checkFriends = CTX.Friends.SingleOrDefault(f => ((f.RequestUser.Id == requestUser.Id && f.ConfirmUser.Id == Friend.Id) || (f.ConfirmUser.Id == requestUser.Id && f.RequestUser.Id == Friend.Id)) && f.Confirme != null);
        //    if (checkFriends != null)
        //    {
        //        CTX.Friends.Remove(checkFriends);
        //        CTX.SaveChanges();

        //        var oponentConnectionIds = requestUser.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientFriendDelete();
        //        }

        //        Clients.Caller.onFriendDelete();
        //    }
        //    else
        //    {
        //        var ansvr = "неясная ошибка. такго друга у вас нету!";
        //        Clients.Caller.onNotExistFriendConfirm(ansvr);
        //    }
        //}
        ///* --- Окончание блока Друзей ---  */
        ///* --- Начало блока Добавления в Бан и Удаления из Бана ---*/

        //public void banAdd(string mac, int banUserId)
        //{
        //    var User = PersonalizationService.GetCurrentUserInfo(mac).User;
        //    var Banner = CTX.SocialUsers.SingleOrDefault(su => su.User.Id == User.Id);
        //    var requestUser = CTX.SocialUsers.Find(banUserId);
        //}
        /* --- Окончание блока Бан ---  */
    }
}