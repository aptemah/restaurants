using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class SocialUser
    {
        public int Id { get; set; }
        public bool? isShown { get; set; }
        public virtual RestAppUser RestUser { get; set; }
        public DateTimeOffset LastActivityDate { get; set; }
        public virtual ICollection<Connections> Connections { get; set; }
        public virtual ICollection<ChatParticipant> Participants { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
        public virtual ICollection<ChatMsgRecipient> Recipients { get; set; }
        public virtual ICollection<ChatFriend> RequestFriends { get; set; }
        public virtual ICollection<ChatFriend> ConfirmFriends { get; set; }
    }
}