using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset Date { get; set; }
        public string AdminFeedBack { get; set; }
        public DateTimeOffset? AdminFeedBackDate { get; set; }
        public virtual RestPoint RestPoint { get; set; }
        public virtual RestAppUser RestAppUser { get; set; }
    }
}