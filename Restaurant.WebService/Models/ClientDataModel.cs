using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intouch.Core;

namespace Intouch.Restaurant
{
    public enum ReadWriteData
    {
        Read = 0,
        Write = 1
    }
    public class ClientDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ActivityLastMonth { get; set; }
        public int Bonus { get; set; }
        public string LastActivityDate { get; set; }
        public string Status { get; set; }
    }
}