using System;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Intouch.Core;
using Microsoft.Ajax.Utilities;

namespace Intouch.SocialNetwork.Hubs
{
    public class FitnessHub : Hub
    {
        //protected readonly CoreContext _db = new CoreContext();

        //public void Connect(Guid sessionId, int point)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var role = user.RoleUser;
        //    var connection = Context.ConnectionId;

        //    var socialUserExist = _db.SocialUsers.Any(su => su.FitnessUser.Id == user.Id);
        //    if (!socialUserExist)
        //    {
        //        var newSocialUser = new SocialUser {FitnessUser = user, LastActivityDate = DateTimeOffset.UtcNow};
        //        var newConnection = new Connections
        //        {
        //            SUser = newSocialUser,
        //            ConnectionId = connection,
        //            ConnectionTime = DateTimeOffset.UtcNow
        //        };
        //        _db.SocialUsers.Add(newSocialUser);
        //        _db.Connections.Add(newConnection);
        //        _db.SaveChanges();
        //        //var newUserInfo = db.SocialUsers.Where(su => su.Id == newSocialUser.Id).Select(su => new
        //        //{
        //        //    FitnessUserId = su.FitnessUser.Id,
        //        //    SocialUserId = su.Id,
        //        //    Name = su.FitnessUser.Name,
        //        //    Photo = su.FitnessUser.Photo,
        //        //    LastActivity = su.LastActivityDate,
        //        //    Connection = su.Connections.Select(c => c.ConnectionId)
        //        //}).SingleOrDefault();

        //        //Clients.AllExcept(connection).onNewUserConnected(newUserInfo);
        //    }
        //    else
        //    {
        //        var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //        var oldConnections = _db.Connections.Where(c => c.SUser.Id == socialUser.Id);
        //        foreach (var toOldConnection in oldConnections)
        //        {
        //            if (toOldConnection.ConnectionTime < DateTimeOffset.Now.AddDays(-1))
        //            {
        //                _db.Connections.Remove(toOldConnection);
        //            }
        //        }

        //        var newConnection = new Connections
        //        {
        //            SUser = socialUser,
        //            ConnectionId = connection,
        //            ConnectionTime = DateTimeOffset.UtcNow
        //        };

        //        _db.Connections.Add(newConnection);
        //        socialUser.LastActivityDate = DateTimeOffset.UtcNow;
        //        _db.SaveChanges();

        //        //var userInfoForClient = db.SocialUsers.Where(su => su.FitnessUser.Id == user.Id).Select(su => new
        //        //{
        //        //    FitnessUserId = su.FitnessUser.Id,
        //        //    SocialUserId = su.Id,
        //        //    Name = su.FitnessUser.Name,
        //        //    Photo = su.FitnessUser.Photo,
        //        //    LastActivity = su.LastActivityDate,
        //        //    Connection = su.Connections.Select(c => c.ConnectionId)
        //        //}).SingleOrDefault();

        //        //Clients.AllExcept(connection).onOldUserConnected(userInfoForClient);
        //    }


        //    switch (role)
        //    {
        //        case Role.User:
        //            if (point != 0)
        //            {
        //                var network = _db.Points.Single(p => p.Id == point).Network.Id;
        //                var socialUserData = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //                var usersInClub =
        //                    _db.SocialUsers.Where(u => u.FitnessUser.FitAppUserPoints.Any(p => p.Point.Id == point) &&
        //                                               u.FitnessUser.Photo != null &&
        //                                               //не показывать тех у кого нет фото
        //                                               u.FitnessUser.Name != null &&
        //                                               // не показывать тех у кого нет имени
        //                                               u.FitnessUser.Id != user.Id && // не показывать себя
        //                                               u.FitnessUser.RoleUser == Role.User)
        //                        .Select(p => new
        //                        {
        //                            FinessUserId = p.FitnessUser.Id,
        //                            SocialUserId = p.Id,
        //                            UsersName = p.FitnessUser.Name,
        //                            LastActivity = p.LastActivityDate,
        //                            UsersPhoto = p.FitnessUser.Photo,
        //                            Connection = p.Connections.Select(c => c.ConnectionId)
        //                        });
        //                var friends = _db.Friends.Where(
        //                    f =>
        //                        (f.ConfirmUser.FitnessUser.FitAppUserPoints.Any(up => up.Point.Network.Id == network) &&
        //                         (f.RequestUser.FitnessUser.FitAppUserPoints.Any(up => up.Point.Network.Id == network))) &&
        //                        f.Confirme != null &&
        //                        (f.RequestUser.Id == socialUserData.Id || f.ConfirmUser.Id == socialUserData.Id))
        //                    .Select(f => new
        //                    {
        //                        UserId =
        //                            f.RequestUser.Id != socialUserData.Id
        //                                ? f.RequestUser.FitnessUser.Id
        //                                : f.ConfirmUser.FitnessUser.Id,
        //                        SocialUserId =
        //                            f.RequestUser.Id != socialUserData.Id ? f.RequestUser.Id : f.ConfirmUser.Id,
        //                        Name =
        //                            f.RequestUser.Id != socialUserData.Id
        //                                ? f.RequestUser.FitnessUser.Name
        //                                : f.ConfirmUser.FitnessUser.Name,
        //                        LastActivity =
        //                            f.RequestUser.Id != socialUserData.Id
        //                                ? f.RequestUser.LastActivityDate
        //                                : f.ConfirmUser.LastActivityDate,
        //                        Photo =
        //                            f.RequestUser.Id != socialUserData.Id
        //                                ? f.RequestUser.FitnessUser.Photo
        //                                : f.ConfirmUser.FitnessUser.Photo,
        //                        AllMsgs =
        //                            _db.ChatMsgRecipients.Count(
        //                                cmr => cmr.Recipient.Id == socialUserData.Id && cmr.Recieved == null),
        //                        Msgs =
        //                            _db.ChatMsgRecipients.Count(
        //                                cmr =>
        //                                    (cmr.Recipient.Id == socialUserData.Id && cmr.Recieved == null &&
        //                                     cmr.Message.Sender.Id == f.RequestUser.Id) ||
        //                                    (cmr.Recipient.Id == socialUserData.Id && cmr.Recieved == null &&
        //                                     cmr.Message.Sender.Id == f.ConfirmUser.Id))
        //                    }).OrderByDescending(f => f.Msgs);

        //                var friendsConfirms =
        //                    _db.Friends.Where(f => f.ConfirmUser.Id == socialUserData.Id &&
        //                                           f.RequestUser.FitnessUser.FitAppUserPoints.Any(
        //                                               up => up.Point.Network.Id == network) &&
        //                                           f.Confirme == null)
        //                        .Select(su => new
        //                        {
        //                            UserId = su.RequestUser.FitnessUser.Id,
        //                            SocialUserId = su.RequestUser.Id,
        //                            FriendsName = su.RequestUser.FitnessUser.Name,
        //                            FriendsPhoto = su.RequestUser.FitnessUser.Photo,
        //                            LastActivity = su.RequestUser.LastActivityDate,
        //                            Connection = su.RequestUser.Connections.Select(c => c.ConnectionId)
        //                        });

        //                var friendsRequests =
        //                    _db.Friends.Where(f => f.RequestUser.Id == socialUserData.Id &&
        //                                           f.ConfirmUser.FitnessUser.FitAppUserPoints.Any(
        //                                               up => up.Point.Network.Id == network) &&
        //                                           f.Confirme == null)
        //                        .Select(su => new
        //                        {
        //                            UserId = su.ConfirmUser.FitnessUser.Id,
        //                            SocialUserId = su.ConfirmUser.Id,
        //                            FriendsName = su.ConfirmUser.FitnessUser.Name,
        //                            FriendsPhoto = su.ConfirmUser.FitnessUser.Photo,
        //                            LastActivity = su.ConfirmUser.LastActivityDate,
        //                            Connection = su.ConfirmUser.Connections.Select(c => c.ConnectionId)
        //                        });
        //                var trainers = _db.FitInstructors
        //                    .Where(i => i.Student.Id == user.Id)
        //                    .Where(i => i.Confirme == null)
        //                    .Where(i => i.Instructor.Network == user.Network)
        //                    .Where(i => i.Call)
        //                    .Select(i => new
        //                    {
        //                        MasterId = i.Instructor.Id,
        //                        ProfileId = _db.FitStaffs.FirstOrDefault(s => s.FitAppUser.Id == i.Instructor.Id).Id,
        //                        Photo = _db.FitStaffs.FirstOrDefault(s => s.FitAppUser.Id == i.Instructor.Id).Photo1,
        //                        MasterName = i.Instructor.Name,
        //                        //MasterRegaly =
        //                        //db.FitStaffs.FirstOrDefault(s => s.FitAppUser.Id == i.Instructor.Id).Regaly,
        //                        SocialMasterId =
        //                            _db.SocialUsers.FirstOrDefault(s => s.FitnessUser.Id == i.Instructor.Id).Id,
        //                        Msgs =
        //                            _db.ChatMsgRecipients.Count(
        //                                s => s.Recipient.FitnessUser.Id == user.Id && s.Recieved == null)
        //                    });
        //                Clients.Caller.onConnected(usersInClub, friends, friendsConfirms, friendsRequests, trainers);
        //            }
        //            else
        //            {
        //                Clients.Caller.onConnected();
        //            }
        //            break;
        //        case Role.Staff:
        //            if (point != 0)
        //            {
        //                var network = _db.Points.Single(p => p.Id == point).Network.Id;

        //                var usersInClub =
        //                    _db.SocialUsers.Where(u => u.FitnessUser.FitAppUserPoints.Any(p => p.Point.Id == point) &&
        //                                               u.FitnessUser.Photo != null &&
        //                                               //не показывать тех у кого нет фото
        //                                               u.FitnessUser.Name != null &&
        //                                               // не показывать тех у кого нет имени
        //                                               u.FitnessUser.Id != user.Id && // не показывать себя
        //                                               u.FitnessUser.RoleUser == Role.User) // не показывать персонал
        //                        .Select(p => new
        //                        {
        //                            FinessUserId = p.FitnessUser.Id,
        //                            SocialUserId = p.Id,
        //                            UserName = p.FitnessUser.Name,
        //                            LastActivity = p.LastActivityDate,
        //                            UserPhoto = p.FitnessUser.Photo,
        //                            Connection = p.Connections.Select(c => c.ConnectionId),
        //                            Msgs =
        //                                _db.ChatMsgRecipients.Count(
        //                                    cmr =>
        //                                        cmr.Recieved == null && cmr.Recipient.FitnessUser.Id == user.Id &&
        //                                        cmr.Message.Sender.FitnessUser.Id == p.FitnessUser.Id)
        //                        }).OrderByDescending(o => o.Msgs).ThenByDescending(o => o.LastActivity);

        //                var usersInNetwork =
        //                    _db.SocialUsers.Where(
        //                        u =>
        //                            u.FitnessUser.FitAppUserPoints.Any(
        //                                p => p.Point.Id != point && p.Point.Network.Id == network) &&
        //                            // все кроме этого поинта в том числе другие сети
        //                            u.FitnessUser.Photo != null && //не показывать тех у кого нет фото
        //                            u.FitnessUser.Name != null && // не показывать тех у кого нет имени
        //                            u.FitnessUser.Id != user.Id && // не показывать себя
        //                            u.FitnessUser.RoleUser == Role.User) // не показывать персонал
        //                        .Select(p => new
        //                        {
        //                            FinessUserId = p.FitnessUser.Id,
        //                            SocialUserId = p.Id,
        //                            UserName = p.FitnessUser.Name,
        //                            LastActivity = p.LastActivityDate,
        //                            UserPhoto = p.FitnessUser.Photo,
        //                            Connection = p.Connections.Select(c => c.ConnectionId),
        //                            Msgs =
        //                                _db.ChatMsgRecipients.Count(
        //                                    cmr =>
        //                                        cmr.Recieved == null && cmr.Recipient.FitnessUser.Id == user.Id &&
        //                                        cmr.Message.Sender.FitnessUser.Id == p.FitnessUser.Id)
        //                        }).OrderByDescending(o => o.Msgs).ThenByDescending(o => o.LastActivity);

        //                var students = _db.FitAppUsers.Where(
        //                    u =>
        //                        u.Students.Any(i => i.Instructor.Id == user.Id && i.Confirme != null) &&
        //                        u.Network == network).Select(u => new
        //                        {
        //                            FinessUserId = u.Id,
        //                            SocialUserId = _db.SocialUsers.FirstOrDefault(su => su.FitnessUser.Id == u.Id).Id,
        //                            StudentsName = u.Name,
        //                            StudentsPhoto = u.Photo,
        //                            Msgs =
        //                                _db.ChatMsgRecipients.Count(
        //                                    cmr =>
        //                                        cmr.Recieved == null && cmr.Recipient.FitnessUser.Id == user.Id &&
        //                                        cmr.Message.Sender.FitnessUser.Id == u.Id)

        //                        }).OrderByDescending(o => o.Msgs);

        //                var notConfirmStudents = _db.FitAppUsers.Where(
        //                    u =>
        //                        u.Students.Any(i => i.Instructor.Id == user.Id && i.Confirme == null && i.Call) &&
        //                        u.Network == network).Select(u => new
        //                        {
        //                            FinessUserId = u.Id,
        //                            SocialUserId = _db.SocialUsers.FirstOrDefault(su => su.FitnessUser.Id == u.Id).Id,
        //                            StudentsName = u.Name,
        //                            StudentsPhoto = u.Photo,
        //                            Msgs =
        //                                _db.ChatMsgRecipients.Count(
        //                                    cmr =>
        //                                        cmr.Recieved == null && cmr.Recipient.FitnessUser.Id == user.Id &&
        //                                        cmr.Message.Sender.FitnessUser.Id == u.Id)

        //                        }).OrderByDescending(o => o.Msgs);

        //                var requestFromStudents = _db.FitAppUsers.Where(
        //                    u =>
        //                        u.Students.Any(i => i.Instructor.Id == user.Id && i.Confirme == null && !i.Call) &&
        //                        u.Network == network).Select(u => new
        //                        {
        //                            FinessUserId = u.Id,
        //                            SocialUserId = _db.SocialUsers.FirstOrDefault(su => su.FitnessUser.Id == u.Id).Id,
        //                            StudentsName = u.Name,
        //                            StudentsPhoto = u.Photo,
        //                            Msgs =
        //                                _db.ChatMsgRecipients.Count(
        //                                    cmr =>
        //                                        cmr.Recieved == null && cmr.Recipient.FitnessUser.Id == user.Id &&
        //                                        cmr.Message.Sender.FitnessUser.Id == u.Id)
        //                        }).OrderByDescending(o => o.Msgs);

        //                Clients.Caller.onStaffConnected(usersInClub, students, notConfirmStudents, usersInNetwork,
        //                    requestFromStudents);
        //            }
        //            break;
        //    }
        //}
        //public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        //{
        //    var connection = Context.ConnectionId;
        //    var connectionString = _db.Connections.SingleOrDefault(c => c.ConnectionId == connection);
        //    _db.Connections.Remove(connectionString);
        //    _db.SaveChanges();
        //    Clients.All.onUserDisconnected();
        //    return base.OnDisconnected(stopCalled);
        //}
        //public void People(int oponentId, Guid sessionId)
        //{
        //    var friendStatus = "default";
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var fituser = session.FitAppUser;
        //    var user = _db.SocialUsers.Single(u => u.FitnessUser.Id == fituser.Id);
        //    var role = fituser.RoleUser;
        //    var oponentRole = _db.SocialUsers.Single(u => u.Id == oponentId).FitnessUser;
        //    switch (role)
        //    {
        //        case Role.User:
        //            if (fituser.Name == null)
        //            {
        //                friendStatus = "noPersonalData";
        //            }
        //            else if (fituser.Photo == null)
        //            {
        //                friendStatus = "noPersonalData";
        //            }
        //            else
        //            {
        //                if (oponentRole.RoleUser == Role.Staff)
        //                {
        //                    friendStatus = "stuff";
        //                }
        //                else
        //                {
        //                    var friend =
        //                        _db.Friends.Any(
        //                            f =>
        //                                f.Confirme != null &&
        //                                ((f.ConfirmUser.Id == user.Id && f.RequestUser.Id == oponentId) ||
        //                                 (f.RequestUser.Id == user.Id && f.ConfirmUser.Id == oponentId)));
        //                    if (friend)
        //                    {
        //                        friendStatus = "friend";
        //                    }
        //                    else
        //                    {
        //                        var addconf =
        //                            _db.Friends.Any(
        //                                f =>
        //                                    (f.ConfirmUser.Id == user.Id && f.RequestUser.Id == oponentId) ||
        //                                    (f.RequestUser.Id == user.Id && f.ConfirmUser.Id == oponentId));
        //                        if (!addconf)
        //                        {
        //                            friendStatus = "user";
        //                        }
        //                        else
        //                        {
        //                            var request =
        //                                _db.Friends.Any(
        //                                    f => f.ConfirmUser.Id == user.Id && f.RequestUser.Id == oponentId);
        //                            friendStatus = request ? "request" : "confirm";
        //                        }
        //                    }
        //                }
        //            }
        //            break;
        //        case Role.Staff:
        //            var student =
        //                _db.FitInstructors.SingleOrDefault(
        //                    i => i.Instructor.Id == fituser.Id && i.Student.Id == oponentRole.Id && i.Confirme != null);
        //            if (student != null)
        //            {
        //                friendStatus = "student";
        //            }
        //            else
        //            {
        //                var appUser =
        //                    _db.FitInstructors.SingleOrDefault(
        //                        i => i.Instructor.Id == fituser.Id && i.Student.Id == oponentRole.Id);
        //                if (appUser == null) friendStatus = "appUser";
        //                else
        //                {
        //                    var requests =
        //                        _db.FitInstructors.SingleOrDefault(
        //                            i =>
        //                                i.Instructor.Id == fituser.Id && i.Student.Id == oponentRole.Id &&
        //                                i.Confirme == null && i.Call);
        //                    friendStatus = requests != null ? "RequestFromMaster" : "RequestToMaster";
        //                }
        //            }
        //            break;
        //    }

        //    var conversation =
        //        _db.Conversations.Any(
        //            c =>
        //                c.Participants.Any(p => p.SUser.Id == oponentId) &&
        //                c.Participants.Any(p => p.SUser.Id == user.Id));
        //    var su = _db.SocialUsers.Single(u => u.Id == oponentId);
        //    var people = new
        //    {
        //        UserId = su.FitnessUser.Id,
        //        SocialUserId = su.Id,
        //        UserName = su.FitnessUser.Name,
        //        LastActivity = su.LastActivityDate,
        //        UserPhoto = su.FitnessUser.Photo,
        //        Age = su.FitnessUser.AgeUser,
        //        Gender = su.FitnessUser.SexOfAppUser,
        //        CustomInterest = su.FitnessUser.CustomInterests,
        //        Interests = su.FitnessUser.FitInterestses.Select(i => new
        //        {
        //            InterestId = i.Id,
        //            InterestName = i.Name,
        //            InterestDescription = i.Description
        //        }),
        //        Connection = su.Connections.Select(c => c.ConnectionId),
        //        status = friendStatus,
        //        Conversation = conversation,
        //        unreadedMsgs =
        //            su.Messages.Count(m => m.Recipients.Any(r => r.Recipient.Id == user.Id && r.Recieved == null)),
        //        me = su.Id == user.Id
        //    };

        //    Clients.Caller.onEnterProfile(people);
        //}
        ///* -- Блок Друзей. Добавление, Удаление, Подтверждение*/
        //// Пока прописаны только caller тоесть уведомление человеку которого добавляют в друзья нету. Так же как нету уведомления человеку которого подтвердили, что он друг.
        //public void FriendRequest(Guid sessionId, int confirmFriendId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var requestFriendUser = session.FitAppUser;
        //    var requestFriend = _db.SocialUsers.Single(su => su.FitnessUser.Id == requestFriendUser.Id);
        //    var confirmFriend = _db.SocialUsers.Find(confirmFriendId);
        //    var checkFriends =
        //        _db.Friends.SingleOrDefault(
        //            f => f.RequestUser.Id == requestFriend.Id && f.ConfirmUser.Id == confirmFriendId);
        //    if (checkFriends == null)
        //    {
        //        var newFriends = new ChatFriend
        //        {
        //            ConfirmUser = confirmFriend,
        //            RequestUser = requestFriend,
        //            Request = DateTimeOffset.UtcNow
        //        };

        //        _db.Friends.Add(newFriends);
        //        _db.SaveChanges();
        //        var oponentConnectionIds = confirmFriend.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientFriendRequest(
        //                    new
        //                    {
        //                        Name = requestFriend.FitnessUser.Name,
        //                        SocialUserId = requestFriend.Id,
        //                        Photo = requestFriend.FitnessUser.Photo
        //                    }, confirmFriend.Id);
        //        }
        //        Clients.Caller.onFriendRequest();
        //    }
        //    else
        //    {
        //        const string ansvr = "Запрос уже послан на рассмотрение";
        //        Clients.Caller.onExist(ansvr);
        //    }
        //}
        //public void FriendConfirm(Guid sessionId, int requestedSocialUserId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var confirmFriendUser = session.FitAppUser;
        //    var confirmFriend = _db.SocialUsers.Single(su => su.FitnessUser.Id == confirmFriendUser.Id);
        //    var requestUser = _db.SocialUsers.Find(requestedSocialUserId);

        //    var checkFriends =
        //        _db.Friends.SingleOrDefault(
        //            f =>
        //                f.RequestUser.Id == requestUser.Id && f.ConfirmUser.Id == confirmFriend.Id && f.Confirme == null);
        //    if (checkFriends != null)
        //    {
        //        checkFriends.Confirme = DateTimeOffset.UtcNow;
        //        _db.SaveChanges();
        //        var oponentConnectionIds = requestUser.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientFriendConfirm(
        //                    new
        //                    {
        //                        Name = confirmFriend.FitnessUser.Name,
        //                        SocialUserId = confirmFriend.Id,
        //                        Photo = confirmFriend.FitnessUser.Photo
        //                    }, requestUser.Id);
        //        }
        //        Clients.Caller.onFriendConfirm();
        //    }
        //    else
        //    {
        //        const string ansvr = "неясная ошибка. такой друг к вам не добавлялся!";
        //        Clients.Caller.onNotExist(ansvr);
        //    }
        //}
        //public void FriendDiscard(Guid sessionId, int deleteSocialUserId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var friendUser = session.FitAppUser;
        //    var friend = _db.SocialUsers.Single(su => su.FitnessUser.Id == friendUser.Id);

        //    var requestUser = _db.SocialUsers.Find(deleteSocialUserId);

        //    var checkFriends =
        //        _db.Friends.SingleOrDefault(f => f.RequestUser.Id == requestUser.Id && f.ConfirmUser.Id == friend.Id);
        //    if (checkFriends != null)
        //    {
        //        _db.Friends.Remove(checkFriends);
        //        _db.SaveChanges();

        //        var oponentConnectionIds = requestUser.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientFriendDiscard(friend.Id, requestUser.Id);
        //        }

        //        Clients.Caller.onFriendDiscard();
        //    }
        //    else
        //    {
        //        const string ansvr = "неясная ошибка. такго друга у вас нету!";
        //        Clients.Caller.onNotExistFriendConfirm(ansvr);
        //    }
        //}
        //public void FriendDelete(Guid sessionId, int deleteSocialUserId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var friendUser = session.FitAppUser;
        //    var friend = _db.SocialUsers.Single(su => su.FitnessUser.Id == friendUser.Id);
        //    var requestUser = _db.SocialUsers.Find(deleteSocialUserId);

        //    var checkFriends =
        //        _db.Friends.SingleOrDefault(
        //            f =>
        //                ((f.RequestUser.Id == requestUser.Id && f.ConfirmUser.Id == friend.Id) ||
        //                 (f.ConfirmUser.Id == requestUser.Id && f.RequestUser.Id == friend.Id)) && f.Confirme != null);
        //    if (checkFriends != null)
        //    {
        //        _db.Friends.Remove(checkFriends);
        //        _db.SaveChanges();

        //        var oponentConnectionIds = requestUser.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientFriendDelete(friend.Id, requestUser.Id);
        //        }

        //        Clients.Caller.onFriendDelete();
        //    }
        //    else
        //    {
        //        const string ansvr = "неясная ошибка. такго друга у вас нету!";
        //        Clients.Caller.onNotExistFriendConfirm(ansvr);
        //    }
        //}
        //public void DeleteRequest(Guid sessionId, int oponentId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var oponent = _db.SocialUsers.Find(oponentId);

        //    var findRequest =
        //        _db.Friends.SingleOrDefault(
        //            f => f.ConfirmUser.Id == oponent.Id && f.RequestUser.Id == socialUser.Id && f.Confirme == null);

        //    if (findRequest != null)
        //    {
        //        _db.Friends.Remove(findRequest);
        //        _db.SaveChanges();
        //        var oponentConnectionIds = oponent.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientDeleteRequest(socialUser.Id, oponent.Id);
        //        }

        //        Clients.Caller.onDeleteRequest();
        //    }
        //}

        ///* --- Окончание блока Друзей ---  */
        ///* --- Блок учеников со стороны тренера---*/

        //public void StudentDelete(Guid sessionId, int studentId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var student = _db.SocialUsers.Find(studentId);
        //    var fitAppStudent = _db.SocialUsers.Find(studentId).FitnessUser;

        //    var instructor =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme != null && i.Instructor.Id == user.Id && i.Student.Id == fitAppStudent.Id);
        //    if (instructor != null)
        //    {
        //        _db.FitInstructors.Remove(instructor);
        //        _db.SaveChanges();
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == student.Id) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        var oponentConnectionIds = student.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientDeleteStudent(socialUser.Id, student.Id, conversation);
        //        }

        //        Clients.Caller.onDeleteStudent(conversation);
        //    }
        //}
        //public void StudentAdd(Guid sessionId, int studentId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return; //break;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var student = _db.SocialUsers.Find(studentId);

        //    var instructor =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Instructor.Id == user.Id && i.Student.Id == student.FitnessUser.Id);
        //    if (instructor == null)
        //    {

        //        var newInstrucktor = new FitInstructor
        //        {
        //            Instructor = user,
        //            Student = student.FitnessUser,
        //            Request = DateTimeOffset.UtcNow,
        //            Call = true
        //        };
        //        _db.FitInstructors.Add(newInstrucktor);
        //        _db.SaveChanges();
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == student.Id) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        var oponentConnectionIds = student.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientAddStudent(socialUser.Id, student.Id, conversation);
        //        }
        //        Clients.Caller.onAddStudent(conversation);
        //    }
        //}
        //public void SubmitClient(Guid sessionId, int clientId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var student = _db.SocialUsers.Find(clientId);
        //    var fitAppClient = _db.SocialUsers.Find(clientId).FitnessUser;
        //    var instructor =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme == null && i.Instructor.Id == user.Id && i.Student.Id == fitAppClient.Id);
        //    if (instructor != null)
        //    {
        //        instructor.Confirme = DateTimeOffset.UtcNow;
        //        _db.SaveChanges();
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == student.Id) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        var oponentConnectionIds = student.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientSubmitClient(socialUser.Id, student.Id, conversation);
        //        }

        //        Clients.Caller.onSubmitClient();
        //    }
        //}
        //public void DiscardStudent(Guid sessionId, int studentId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var student = _db.SocialUsers.Find(studentId);
        //    var fitAppStudent = _db.SocialUsers.Find(studentId).FitnessUser;
        //    var instructor =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme == null && i.Instructor.Id == user.Id && i.Student.Id == fitAppStudent.Id);
        //    if (instructor != null)
        //    {
        //        _db.FitInstructors.Remove(instructor);
        //        _db.SaveChanges();
        //        var oponentConnectionIds = student.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientDiscardStudent(socialUser.Id);
        //        }

        //        Clients.Caller.onDiscardStudent();
        //    }
        //}
        //public void DiscardClient(Guid sessionId, int studentId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var student = _db.SocialUsers.Find(studentId);
        //    var fitAppStudent = _db.SocialUsers.Find(studentId).FitnessUser;
        //    var instructor =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme == null && i.Instructor.Id == user.Id && i.Student.Id == fitAppStudent.Id);
        //    if (instructor != null)
        //    {
        //        _db.FitInstructors.Remove(instructor);
        //        _db.SaveChanges();
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == student.Id) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        var oponentConnectionIds = student.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientDiscardClient(socialUser.Id, student.Id, conversation);
        //        }

        //        Clients.Caller.onDiscardClient(conversation);
        //    }
        //}

        ///* --- Окончание блока Учеников со стороны тренера ---  */
        ///* --- Блок учеников со стороны клиента---*/

        //public void DiscardInstructor(Guid sessionId, int instructorId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var instructor = _db.SocialUsers.Find(instructorId);
        //    var fitAppInstructor = _db.SocialUsers.Find(instructorId).FitnessUser;

        //    var instructors =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme == null && i.Instructor.Id == fitAppInstructor.Id && i.Student.Id == user.Id);
        //    if (instructors != null)
        //    {
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == instructorId) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        _db.FitInstructors.Remove(instructors);
        //        _db.SaveChanges();

        //        var oponentConnectionIds = instructor.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientDiscardInstructor(socialUser.Id, instructor.Id, conversation);
        //        }

        //        Clients.Caller.onDiscardInstructor();
        //    }
        //}
        //public void SubmitInstructor(Guid sessionId, int instructorId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var instructor = _db.SocialUsers.Find(instructorId);
        //    var fitAppInstructor = _db.SocialUsers.Find(instructorId).FitnessUser;

        //    var instructors =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme == null && i.Instructor.Id == fitAppInstructor.Id && i.Student.Id == user.Id);
        //    if (instructors != null)
        //    {
        //        instructors.Confirme = DateTimeOffset.UtcNow;
        //        _db.SaveChanges();
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == instructorId) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        var oponentConnectionIds = instructor.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientSubmitStudent(socialUser.Id, instructor.Id, conversation);
        //        }

        //        Clients.Caller.onSubmitStudent();
        //    }
        //}
        //public void DeleteInstructor(Guid sessionId, int instructorId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var instructor = _db.SocialUsers.Find(instructorId);
        //    var fitAppInstructor = _db.SocialUsers.Find(instructorId).FitnessUser;

        //    var instructors =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme != null && i.Instructor.Id == fitAppInstructor.Id && i.Student.Id == user.Id);
        //    if (instructors != null)
        //    {
        //        _db.FitInstructors.Remove(instructors);
        //        _db.SaveChanges();
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == instructorId) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        var oponentConnectionIds = instructor.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId)
        //                .onClientDeleteInstructor(socialUser.Id, instructor.Id, conversation);
        //        }

        //        Clients.Caller.onDeleteInstructor();
        //    }
        //}
        //public void AddMaster(Guid sessionId, int masterId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var master = _db.SocialUsers.Find(masterId);

        //    var instructor =
        //        _db.FitInstructors.SingleOrDefault(i => i.Instructor.Id == master.Id && i.Student.Id == user.Id);
        //    if (instructor == null)
        //    {
        //        var newInstrucktor = new FitInstructor
        //        {
        //            Instructor = master.FitnessUser,
        //            Student = user,
        //            Request = DateTimeOffset.UtcNow
        //        };
        //        _db.FitInstructors.Add(newInstrucktor);
        //        _db.SaveChanges();
        //        var conversation =
        //            _db.Conversations.Any(
        //                c =>
        //                    c.Participants.Count() == 2 && c.Participants.Any(p => p.SUser.Id == masterId) &&
        //                    c.Participants.Any(p => p.SUser.Id == socialUser.Id));
        //        var oponentConnectionIds = master.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientAddMaster(socialUser.Id, master.Id, conversation);
        //        }
        //        Clients.Caller.onAddMaster();
        //    }
        //}
        //public void DiscardCallToMaster(Guid sessionId, int masterId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var instructor = _db.SocialUsers.Find(masterId);
        //    var fitAppInstructor = _db.SocialUsers.Find(masterId).FitnessUser;

        //    var instructors =
        //        _db.FitInstructors.SingleOrDefault(
        //            i => i.Confirme == null && i.Instructor.Id == fitAppInstructor.Id && i.Student.Id == user.Id);
        //    if (instructors != null)
        //    {
        //        _db.FitInstructors.Remove(instructors);
        //        _db.SaveChanges();
        //        var oponentConnectionIds = instructor.Connections.ToList();
        //        foreach (var connection in oponentConnectionIds)
        //        {
        //            Clients.Client(connection.ConnectionId).onClientDiscardCallToMaster(socialUser.Id);
        //        }

        //        Clients.Caller.onDiscardCallToMaster();
        //    }
        //}
        ///* --- Окончание блока Учеников со стороны клиента ---  */

        //public void ConversationSearch(int toUserId, Guid sessionId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var fromUserIdUserTbl = session.FitAppUser;
        //    var fromUserId = _db.SocialUsers.Single(su => su.FitnessUser.Id == fromUserIdUserTbl.Id).Id;


        //    var conversation =
        //        _db.Conversations.SingleOrDefault(
        //            c =>
        //                c.Participants.Any(p => p.SUser.Id == toUserId) &&
        //                c.Participants.Any(p => p.SUser.Id == fromUserId) && c.Participants.Count() == 2 &&
        //                c.Code == null);



        //    if (conversation == null)
        //    {
        //        var toUser = _db.SocialUsers.Single(u => u.Id == toUserId);
        //        var fromUser = _db.SocialUsers.Single(u => u.Id == fromUserId);
        //        var newConversation = new Conversation {Created = DateTimeOffset.UtcNow};
        //        var participantOne = new ChatParticipant
        //        {
        //            Connect = DateTimeOffset.UtcNow,
        //            Conversation = newConversation,
        //            SUser = toUser
        //        };
        //        var participantTwo = new ChatParticipant
        //        {
        //            Connect = DateTimeOffset.UtcNow,
        //            Conversation = newConversation,
        //            SUser = fromUser
        //        };

        //        _db.Conversations.Add(newConversation);
        //        _db.ChatParticipants.Add(participantOne);
        //        _db.ChatParticipants.Add(participantTwo);
        //        _db.SaveChanges();

        //        Clients.Caller.onConversation(newConversation.Id);
        //    }
        //    else
        //    {
        //        Clients.Caller.onConversation(conversation.Id);
        //    }
        //}
        //public void ConnectToPersonalConversation(int conversationId, Guid sessionId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var participants = _db.ChatParticipants.Where(p => p.Conversation.Id == conversationId).Select(p => new
        //    {
        //        ParticipantsId = p.SUser.Id,
        //        ParticipantsName = p.SUser.FitnessUser.Name,
        //        ConnectTime = p.Connect,
        //        // TODO: status online / offline
        //    }).OrderBy(p => p.ParticipantsId);

        //    var messages = _db.ChatMessages
        //        .Where(m => m.Conversation.Id == conversationId)
        //        .GroupBy(m => new {Year = m.Created.Year, Month = m.Created.Month, Day = m.Created.Day})
        //        .Select(m => new
        //        {
        //            count = m.Count(),
        //            date = m.Key,
        //            status =
        //                m.Key.Day == DateTimeOffset.Now.Day && m.Key.Month == DateTimeOffset.Now.Month &&
        //                m.Key.Year == DateTimeOffset.Now.Year,
        //            MessageData = m.Select(x => new
        //            {
        //                MsgId = x.Id,
        //                Message = x.Messages,
        //                SenderId = x.Sender.Id,
        //                SenderName = x.Sender.FitnessUser.Name,
        //                MsgCreated = x.Created,
        //                Status = x.Sender.Id == socUser.Id ? "my" : "",
        //                Time = new
        //                {
        //                    hour = x.Created.Hour,
        //                    min = x.Created.Minute
        //                }
        //            }).OrderBy(x => x.MsgCreated)
        //        }).OrderBy(m => m.date);

        //    var oponentEntity =
        //        _db.ChatParticipants.Single(
        //            p => p.Conversation.Id == conversationId && p.SUser.Id != socUser.Id);
        //    var staffId = 0;
        //    var staff = _db.FitStaffs.SingleOrDefault(s => s.FitAppUser.Id == oponentEntity.SUser.FitnessUser.Id);
        //    if (staff != null) staffId = staff.Id;
        //    var oponent = new
        //    {
        //        OponentId = oponentEntity.SUser.Id,
        //        OponentName = oponentEntity.SUser.FitnessUser.Name,
        //        ConnectTime = oponentEntity.Connect,
        //        OponentPhoto = oponentEntity.SUser.FitnessUser.Photo,
        //        OponentRole = _db.FitStaffs.Any(s => s.FitAppUser.Id == oponentEntity.SUser.FitnessUser.Id),
        //        OponentStaffId = staffId
        //        // TODO: status online / offline
        //    };

        //    Clients.Caller.onConnectedToConversation(participants, messages, oponent);
        //}
        //public void SendMsgToPrivateConversation(int conversationId, string message, Guid sessionId)
        //{
        //    var sessionEntity = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (sessionEntity == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var sender = sessionEntity.FitAppUser;
        //    var recipientConnections =
        //        _db.Conversations.Single(c => c.Id == conversationId)
        //            .Participants.Single(p => p.SUser.FitnessUser.Id != sender.Id)
        //            .SUser.Connections;

        //    var conversationEntity = _db.Conversations.Single(c => c.Id == conversationId);

        //    var senders = _db.SocialUsers.Single(u => u.FitnessUser.Id == sender.Id);
        //    var recipient =
        //        _db.Conversations.Single(c => c.Id == conversationId)
        //            .Participants.Single(p => p.SUser.FitnessUser.Id != sender.Id)
        //            .SUser;
        //    var date = DateTimeOffset.Now;

        //    var status =
        //        conversationEntity.Messages.Any(
        //            m => m.Created.Day == date.Day && m.Created.Month == date.Month && m.Created.Year == date.Year);

        //    var newMsg = new ChatMessage
        //    {
        //        Conversation = conversationEntity,
        //        Created = date,
        //        Messages = message,
        //        Sender = senders
        //    };
        //    var newRcpt = new ChatMsgRecipient {Message = newMsg, Recipient = recipient};

        //    _db.ChatMessages.Add(newMsg);
        //    _db.ChatMsgRecipients.Add(newRcpt);
        //    _db.SaveChanges();

        //    foreach (var item in recipientConnections)
        //    {
        //        Clients.Client(item.ConnectionId)
        //            .sendPrivateMessageToOponent(
        //                new
        //                {
        //                    message = newMsg.Messages,
        //                    messageId = newMsg.Id,
        //                    conversation = newMsg.Conversation.Id,
        //                    subject = "oponent",
        //                    sender = newMsg.Sender.Id,
        //                    recipient = newRcpt.Recipient.Id,
        //                    time =
        //                        new
        //                        {
        //                            hour = newMsg.Created.Hour,
        //                            min = newMsg.Created.Minute,
        //                            day = newMsg.Created.Day,
        //                            month = newMsg.Created.Month,
        //                            year = newMsg.Created.Year,
        //                            statusMsg = status
        //                        }
        //                });
        //    }

        //    Clients.Caller.sendPrivateMessageToMe(
        //        new
        //        {
        //            message = newMsg.Messages,
        //            messageId = newMsg.Id,
        //            conversation = newMsg.Conversation.Id,
        //            subject = "oponent",
        //            sender = newMsg.Sender.Id,
        //            recipient = newRcpt.Recipient.Id,
        //            time =
        //                new
        //                {
        //                    hour = newMsg.Created.Hour,
        //                    min = newMsg.Created.Minute,
        //                    day = newMsg.Created.Day,
        //                    month = newMsg.Created.Month,
        //                    year = newMsg.Created.Year,
        //                    statusMsg = status
        //                }
        //        });
        //}
        ////public void CountForMessage(Guid SessionId, int ConversationId)
        ////{
        ////    Thread.Sleep(100);
        ////    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == SessionId && s.DateOfEndSession == null);

        ////    var User = session.FitAppUser;
        ////    var Oponent = _db.ChatParticipants.Where(p => p.Conversation.Id == ConversationId && p.SUser.FitnessUser.Id != User.Id).SingleOrDefault();
        ////    var AllMsgsCount = _db.ChatMsgRecipients.Where(r => r.Recieved == null && r.Recipient.Id == Oponent.SUser.Id).Count(); //TODO: проверить на сервере. нужен ли нам +1
        ////    //var toUserId = CTX.ChatParticipants.Where(p => p.Conversation.Id == ConversationId && p.SUser.Id == Oponent.SUser.Id).Select(p => p.SUser.Connections.Select(c => c.ConnectionId)).SingleOrDefault().ToString();
        ////    var toUserId = Oponent.SUser.Connections;
        ////    var Dialogs = _db.Conversations.Where(c => c.Messages.Any(m => m.Recipients.Any(r => r.Recipient.Id == Oponent.SUser.Id && r.Recieved == null))).Count();
        ////    var UserDialogs = _db.ChatMsgRecipients.Where(mr => mr.Recieved == null && mr.Recipient.Id == Oponent.SUser.Id && mr.Message.Sender.FitnessUser.Id == User.Id).GroupBy(mr => mr.Message.Conversation.Id).Select(mr => new
        ////    {
        ////        ConversationId = mr.Key,
        ////        Count = mr.Count(),
        ////        DATA = mr.Select(x => new
        ////        {
        ////            OponentName = x.Message.Sender.FitnessUser.Name,
        ////            OponentId = x.Message.Sender.Id,
        ////            //ConversationId = x.Message.Conversation.Id,
        ////            //ConversationName = x.Message.Conversation.Code
        ////        }).FirstOrDefault(),
        ////        date = mr.Select(xx => xx.Message.Created).FirstOrDefault()
        ////    }).SingleOrDefault();

        ////    foreach (var connection in toUserId)
        ////    {
        ////        Clients.Client(connection.ConnectionId).countMessage(AllMsgsCount, Dialogs, UserDialogs);
        ////    }

        ////}
        //public void Readed(Guid sessionId, int conversationId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var mainUser = session.FitAppUser;
        //    var userId = _db.SocialUsers.Single(su => su.FitnessUser.Id == mainUser.Id).Id;
        //    var msgToRead =
        //        _db.ChatMsgRecipients.Where(
        //            mr =>
        //                mr.Message.Conversation.Id == conversationId && mr.Recipient.Id == userId && mr.Recieved == null);

        //    foreach (var msgs in msgToRead)
        //    {
        //        msgs.Recieved = DateTimeOffset.UtcNow;
        //    }
        //    _db.SaveChanges();
        //}
        //public void IsRead(Guid sessionId, int msgId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUser = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);

        //    var recipient = _db.ChatMsgRecipients.Single(r => r.Message.Id == msgId && r.Recipient.Id == socialUser.Id);

        //    recipient.Recieved = DateTimeOffset.UtcNow;

        //    _db.SaveChanges();

        //    Clients.All.onIsRead("Прочитано епта!");
        //}
        //public void CheckOwnProfile(Guid sessionId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId && s.DateOfEndSession == null);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    if (user.Name == null)
        //    {
        //        Clients.Caller.onCheckOwnProfile(false);
        //    }
        //    else if (user.Photo == null)
        //    {
        //        Clients.Caller.onCheckOwnProfile(false);
        //    }
        //    else
        //    {
        //        Clients.Caller.onCheckOwnProfile(true);
        //    }
        //}
        //public void CountAllMsgs(Guid sessionId) // эта функция выполняется для себя по загрузки страницы
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var socialUserData = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);

        //    var msgs =
        //        _db.ChatMsgRecipients.Count(cmr => cmr.Recipient.Id == socialUserData.Id && cmr.Recieved == null);
        //    var allMsgsFromUser =
        //        _db.ChatMsgRecipients.Count(
        //            cmr =>
        //                cmr.Recipient.Id == socialUserData.Id && cmr.Message.Sender.FitnessUser.RoleUser == Role.User &&
        //                cmr.Recieved == null);
        //    var allMsgsFromStaff =
        //        _db.ChatMsgRecipients.Count(
        //            cmr =>
        //                cmr.Recipient.Id == socialUserData.Id && cmr.Message.Sender.FitnessUser.RoleUser == Role.Staff &&
        //                cmr.Recieved == null);

        //    Clients.Caller.onCountMsgs(msgs, allMsgsFromUser, allMsgsFromStaff);
        //}
        //public void CountForMainPage(Guid sessionId, int conversationId)
        //{
        //    Thread.Sleep(100);
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var oponent =
        //        _db.ChatParticipants.Single(
        //            p => p.Conversation.Id == conversationId && p.SUser.FitnessUser.Id != user.Id);
        //    var oponentConnections = oponent.SUser.Connections;
        //    var allMsgsCount = _db.ChatMsgRecipients.Count(r => r.Recieved == null && r.Recipient.Id == oponent.SUser.Id);
        //    var allMsgsFromUser =
        //        _db.ChatMsgRecipients.Count(
        //            cmr =>
        //                cmr.Recipient.Id == oponent.SUser.Id && cmr.Message.Sender.FitnessUser.RoleUser == Role.User &&
        //                cmr.Recieved == null);
        //    var allMsgsFromStaff =
        //        _db.ChatMsgRecipients.Count(
        //            cmr =>
        //                cmr.Recipient.Id == oponent.SUser.Id && cmr.Message.Sender.FitnessUser.RoleUser == Role.Staff &&
        //                cmr.Recieved == null);

        //    foreach (var connection in oponentConnections)
        //    {
        //        Clients.Client(connection.ConnectionId)
        //            .onCountMsgsForMainPage(allMsgsCount, allMsgsFromUser, allMsgsFromStaff);
        //    }
        //}

        //public void CountMsgsFromInstructor(Guid sessionId, int instructorSocId)
        //    //считает количество сообщений от инструктора на страничке персонала. И узнаем кто этот персонал для нас. (инструктор, обычный тренер, или послал нам запрос!)
        //{
        //    var status = "default";
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;
        //    var role = user.RoleUser;
        //    var socialUserData = _db.SocialUsers.Single(su => su.FitnessUser.Id == user.Id);
        //    var fitAppInstrucktor = _db.SocialUsers.Single(su => su.Id == instructorSocId).FitnessUser;
        //    var msgsFromStaff =
        //        _db.ChatMsgRecipients.Count(
        //            cmr =>
        //                cmr.Recipient.Id == socialUserData.Id && cmr.Message.Sender.Id == instructorSocId &&
        //                cmr.Recieved == null);

        //    if (role == Role.User)
        //    {
        //        var student =
        //            _db.FitInstructors.SingleOrDefault(
        //                i => i.Instructor.Id == fitAppInstrucktor.Id && i.Student.Id == user.Id && i.Confirme != null);
        //        if (student != null)
        //        {
        //            status = "student";
        //        }
        //        else
        //        {
        //            var staff = _db.FitInstructors.SingleOrDefault(
        //                i =>
        //                    i.Instructor.Id == fitAppInstrucktor.Id && i.Student.Id == user.Id && i.Confirme == null);
        //            if (staff == null)
        //            {
        //                status = "staff";
        //            }
        //            else
        //            {
        //                var requestStudent =
        //                    _db.FitInstructors.SingleOrDefault(
        //                        i =>
        //                            i.Instructor.Id == fitAppInstrucktor.Id && i.Student.Id == user.Id &&
        //                            i.Confirme == null && i.Call);
        //                status = requestStudent != null ? "requestFromMaster" : "requestToMaster";
        //            }

        //        }
        //    }
        //    else if (role == Role.Staff)
        //    {
        //        status = "colleague";
        //    }
        //    Clients.Caller.onCountMsgsFromInstructor(msgsFromStaff, status);
        //}
        //public void Messages(Guid sessionId)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var user = session.FitAppUser;

        //    var msg =
        //        _db.Conversations.Where(
        //            c => c.Participants.Any(p => p.SUser.FitnessUser.Id == user.Id) && c.Messages.Any()).ToList().Select(c => new
        //            {
        //                Conversation = c.Id,
        //                Count = c.Messages.Count(),
        //                Unreaded =
        //                    c.Messages.Count(
        //                        m => m.Recipients.Any(r => r.Recieved == null && r.Recipient.FitnessUser.Id == user.Id)),
        //                User = c.Participants.Where(p => p.SUser.FitnessUser.Id != user.Id).Select(p => new
        //                {
        //                    Id = p.SUser.FitnessUser.Id,
        //                    SocId = p.SUser.Id,
        //                    Name = p.SUser.FitnessUser.Name,
        //                    Staff = Status(user, p.SUser.FitnessUser),
        //                    Photo = p.SUser.FitnessUser.Photo
        //                }).FirstOrDefault(),
        //                Date = c.Messages.Select(m => new
        //                {
        //                    date = m.Created,
        //                    day = m.Created.Day,
        //                    month = m.Created.Month,
        //                    year = m.Created.Year
        //                }).OrderByDescending(m => m.date).FirstOrDefault(),
        //            }).OrderByDescending(r => r.Unreaded).ThenByDescending(r => r.Date.date);
        //    Clients.Caller.onMessage(msg);
        //}
        //// тренера человека в профиле
        //public void TrainersMastersOnPeopleProfile(Guid sessionId, int socUser)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var network = session.FitAppUser.Network;
        //    var user = _db.SocialUsers.Single(su => su.Id == socUser).FitnessUser;

        //    var trainers = _db.FitInstructors
        //        .Where(i => i.Student.Id == user.Id)
        //        .Where(i => i.Confirme != null)
        //        .Where(i => i.Instructor.Network == network)
        //        .Select(i => new
        //        {
        //            Id = i.Instructor.Id,
        //            ProfileId = _db.FitStaffs.FirstOrDefault(s => s.FitAppUser.Id == i.Instructor.Id).Id,
        //            Photo = _db.FitStaffs.FirstOrDefault(s => s.FitAppUser.Id == i.Instructor.Id).Photo1,
        //            Name = i.Instructor.Name,
        //            Regaly = _db.FitStaffs.FirstOrDefault(s => s.FitAppUser.Id == i.Instructor.Id).Regaly
        //        });

        //    Clients.Caller.onMastersInPeopleProfile(trainers);
        //}
        //public void ClientsOnMasterPage(Guid sessionId, int master)
        //{
        //    var session = _db.FitAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //    if (session == null)
        //    {
        //        Clients.Caller.onNoSession();
        //        return;
        //    }
        //    var network = session.FitAppUser.Network;
        //    var instructor = _db.SocialUsers.Single(su => su.Id == master).FitnessUser;

        //    var trainers = _db.FitInstructors
        //        .Where(i => i.Instructor.Id == instructor.Id)
        //        .Where(i => i.Confirme != null)
        //        .Where(i => i.Student.Network == network)
        //        .Select(i => new
        //        {
        //            ClientsId = i.Student.Id,
        //            ClientsPhoto = i.Student.Photo,
        //            ClientsName = i.Student.Name,
        //            SocId = _db.SocialUsers.FirstOrDefault(u => u.FitnessUser.Id == i.Student.Id).Id,
        //            me = i.Student.Id == session.FitAppUser.Id
        //        });

        //    Clients.Caller.onClientsOnMasterPage(trainers);
        //}
        //public object Status(FitAppUser user, FitAppUser oponent)
        //{
        //    var socUser = _db.SocialUsers.Single(u => u.FitnessUser.Id == user.Id);
        //    var socOponent = _db.SocialUsers.Single(u => u.FitnessUser.Id == oponent.Id);

        //    switch (user.RoleUser)
        //    {
        //        case Role.Staff:
        //            var client = _db.FitInstructors.SingleOrDefault(i => i.Instructor.Id == user.Id && i.Student.Id == oponent.Id && i.Confirme != null);
        //            return client == null ? new { role = user.RoleUser, status = "user" } : new { role = user.RoleUser, status = "client" };
        //        case Role.User:
        //            var friend = _db.Friends.SingleOrDefault(
        //                f =>
        //                    f.Confirme != null &&
        //                    ((f.ConfirmUser.Id == socUser.Id && f.RequestUser.Id == socOponent.Id) ||
        //                     (f.RequestUser.Id == socUser.Id && f.ConfirmUser.Id == socOponent.Id)));
        //            if (friend != null) return new { role = user.RoleUser, status = "friend" };
        //            return oponent.RoleUser == Role.Staff ? new { role = user.RoleUser, status = "staff" } : new { role = user.RoleUser, status = "user" };
        //    }

        //    return new object {};
        //}
    }
}
