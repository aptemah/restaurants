using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestFeedback
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
        public DateTimeOffset Date { get; set; }
        public virtual RestPoint RestPoint { get; set; }
    }
}