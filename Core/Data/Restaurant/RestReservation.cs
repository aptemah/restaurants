using System;

namespace Intouch.Core
{
    public class RestReservation
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset DateReservation { get; set; }
        public DateTimeOffset DateCreate { get; set; }
        public int People { get; set; }
        public string Comment { get; set; }
        public virtual RestPoint RestPoint { get; set; }
    }
}