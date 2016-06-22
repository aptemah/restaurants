using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum Activity
    {
        Inactive = 0,
        Active = 1
    }
    public class RestMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Activity Activity { get; set; }
        public virtual RestPoint RestPoint { get; set; }
        public virtual ICollection<RestCategory> Categorys { get; set; }
    }
}