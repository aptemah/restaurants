using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestGallery
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public virtual RestPoint RestPoint { get; set; }
    }
}