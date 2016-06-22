using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.SocialNetwork
{
    public class MessagesModel
    {
        public int Conversation { get; set; }
        public int Count { get; set; }
        public int Unreaded { get; set; }
        public UserModel User { get; set; }
        public DateModel Date { get; set; }
        
    }
    public class UserModel
    {
        public int Id { get; set; }
        public int SocId { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
    public class DateModel
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public DateTimeOffset date { get; set; }
    }
}