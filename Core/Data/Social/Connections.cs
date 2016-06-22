using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class Connections
    {
        public int Id { get; set; }
        public virtual SocialUser SUser { get; set; }
        public string ConnectionId { get; set; }
        public DateTimeOffset ConnectionTime { get; set; }
        //public virtual Device Device { get; set; }
    }
}