using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum TypeMessage
    {
        Text = 0,
        Picture = 1
    }

    public class ChatMessage
    {
        public int Id { get; set; }
        public string Messages { get; set; }
        public DateTimeOffset Created { get; set; }
        public TypeMessage TypeMessage { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual SocialUser Sender { get; set; }
        public virtual ICollection<ChatMsgRecipient> Recipients { get; set; }
    }
}