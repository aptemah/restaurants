using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestProdReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public virtual RestProduct RestProduct { get; set; }
        public virtual RestAppUser RestAppUser { get; set; }
    }
}