using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Utils
{
    public class SendMailModel
    {
        public string From { get; set; }
        public string FromPassword { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public SendMailModel()
        {
            From = "info@intouchclub.ru";
            FromPassword = "intouch";
            FromName = "Intouch Club";
        }
    }
}