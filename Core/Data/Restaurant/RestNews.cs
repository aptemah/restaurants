using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestNews
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreate { get; set; }        
        public string Description { get; set; }
        public string SmallDescription { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual RestPoint RestPoint { get; set; }
        public virtual RestNetwork RestNetwork { get; set; }
    }
}