using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum ConfirmationMessage
    {
        [Display(Name = "не используется")]
        NotUsed = 0,
        [Display(Name = "используется")]
        Used = 1,
    }
    public class RestPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Kitchen { get; set; }
        public string WelcomePage { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public int BonusPercent { get; set; }
        public string Background { get; set; }
        public string Description { get; set; }
        public string AveragePrice { get; set; }
        public string WorkTime { get; set; }
        public string Skype { get; set; }
        public string EmailToFeedback { get; set; }
        public string EmailToReservation { get; set; }
        public virtual Point Point { get; set; }        
        public virtual ConfirmationMessage ConfirmationMessage { get; set; }
        public virtual ICollection<RestOrder> RestOrders { get; set; }
        public virtual ICollection<RestReservation> RestReservations { get; set; }
        public virtual ICollection<RestFeedback> RestFeedbacks { get; set; }
        public virtual ICollection<RestMainPhoto> RestMainPhotos { get; set; }
        public virtual ICollection<RestGallery> RestGallerys { get; set; }
        public virtual ICollection<RestReview> RestReviews { get; set; }
        public virtual ICollection<RestMenu> RestMenus { get; set; }
        public virtual ICollection<RestArticle> RestArticles { get; set; }
        public virtual ICollection<RestNews> RestNewses { get; set; }
        public virtual ICollection<RestAdvertisement> RestAdvertisements { get; set; }
    }
}