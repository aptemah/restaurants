using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class ChatMsgRecipient
    {
        public int Id { get; set; }
        public DateTimeOffset? Recieved { get; set; }
        public virtual SocialUser Recipient { get; set; }
        public virtual ChatMessage Message { get; set; }
    }
}