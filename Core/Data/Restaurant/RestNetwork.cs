using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestNetwork
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string VideoUrl { get; set; }
        public string Background { get; set; }
        public string AveragePrice { get; set; }
        public string OpeningTimes { get; set; }
        public string Services { get; set; }
        public string Tariff { get; set; }
        public string OthersServices { get; set; }
        public virtual Network Network { get; set; }
        public virtual ICollection<RestNews> RestNewses { get; set; }
        public virtual ICollection<RestArticle> RestArticles { get; set; }
        public virtual ICollection<RestAdvertisement> RestAdvertisements { get; set; }
    }
}