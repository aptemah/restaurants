using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestAdvertisement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }        
        public string VkLikeLink { get; set; }
        public string FbLikeLink { get; set; }
        public string TwRetwitLink { get; set; }
        public virtual RestPoint RestPoint { get; set; }
        public virtual RestNetwork RestNetwork { get; set; }
    }
}