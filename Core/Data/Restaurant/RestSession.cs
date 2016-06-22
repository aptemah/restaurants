using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestAppSession
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset LastNewsView { get; set; }
        public DateTimeOffset LastOccasionView { get; set; }
        public virtual RestAppUser RestAppUser { get; set; }
    }
}