using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class ChatFriend
    {
        public int Id { get; set; }
        public virtual SocialUser RequestUser { get; set; }
        public virtual SocialUser ConfirmUser { get; set; }
        public DateTimeOffset Request { get; set; }
        public DateTimeOffset? Confirme { get; set; }
    }
}