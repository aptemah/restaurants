using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class ChatParticipant
    {
        public int Id { get; set; }
        public DateTimeOffset Connect { get; set; }
        //public string ConnectionId { get; set; }

        public virtual Conversation Conversation { get; set; }
        public virtual SocialUser SUser { get; set; }
    }
}