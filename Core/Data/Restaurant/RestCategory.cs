using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestCategory //editable list
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Activity Activity { get; set; }
        public virtual RestCategory ParentCategory { get; set; }
        public virtual RestPoint RestPoint { get; set; }
        public virtual ICollection<RestCategory> SubCategories { get; set; }
        public virtual ICollection<RestProduct> Products { get; set; }
    }
}