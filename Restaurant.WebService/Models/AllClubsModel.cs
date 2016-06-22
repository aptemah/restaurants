using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Restaurant
{
    public class AllClubsModel
    {
        public int ClubId { get; set; }
        public int PointId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Background { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double distance { get; set; }
        public string Network { get; set; }
    }
}