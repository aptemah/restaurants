using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestProductFavorite
    {
        public int Id { get; set; }
        public virtual RestAppUser RestAppUser { get; set; }
        public virtual ICollection<RestProduct> RestProducts { get; set; }
    }
}