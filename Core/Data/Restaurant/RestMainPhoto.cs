using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestMainPhoto
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public DateTimeOffset DateCreate { get; set; }
        public RestPoint RestPoint { get; set; }
    }
}