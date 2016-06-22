using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Intouch.Core
{
    public class Point
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LandingPage { get; set; }
        public string Ip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public City? City { get; set; }
        public virtual Network Network { get; set; }
        public virtual ICollection<WifiSession> WifiSessions { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public string Address { get; set; }
        public virtual ICollection<RestPoint> RestPoints { get; set; }

        public string FullName
        {
            get { return string.Format("{0}, {1}", Network.Name, Name); }
        }
    }

    public enum City
    {
        Moscow
    }
}