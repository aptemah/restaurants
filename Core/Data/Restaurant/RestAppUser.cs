using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum RestRole
    {
        User,
        Admin,
        Officiant
    }
    public enum AgeOfRestAppUser
    {
        [Display(Name = "Выберите возраст")]
        Undefined = 0,
        [Display(Name = "Меньше 18")]
        To18 = 1,
        [Display(Name = "18-25")]
        From18To25 = 2,
        [Display(Name = "26-35")]
        From26To35 = 3,
        [Display(Name = "36-45")]
        From36To45 = 4,
        [Display(Name = "45-50")]
        From46To50 = 5,
        [Display(Name = "Больше 50")]
        From50 = 6
    }
    public enum RestSex
    {
        [Display(Name = "Выберите пол")]
        Undefined = 0,
        [Display(Name = "Мужчина")]
        Man = 1,
        [Display(Name = "Женщина")]
        Woman = 2
    }
    public enum CheckUser
    {
        [Display(Name = "не подтвержден")]
        Notconfirmed = 0,
        [Display(Name = "подтвержден")]
        Сonfirmed = 1,
    }
    public class RestAppUser
    {
        [Key]
        [ForeignKey("SocialUser")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RestRole Role { get; set; }
        public AgeOfRestAppUser AgeUser { get; set; }
        public CheckUser CheckUser { get; set; }
        public RestSex Sex { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Photo { get; set; }        
        public virtual RestPayment RestPayment { get; set; }
        public virtual SocialUser SocialUser { get; set; }
        public virtual Point Point { get; set; }
        public virtual ICollection<RestSms> RestSmses { get; set; }
        public virtual ICollection<RestAppSession> RestAppSessions { get; set; }
        public virtual ICollection<RestReview> RestReviews { get; set; }
        public virtual ICollection<RestBonusHistory> RestBonusHistorys { get; set; }
        public virtual ICollection<RestProdReview> RestProdReviews { get; set; }
        public virtual ICollection<RestOrder> RestOrders { get; set; }
        public virtual ICollection<RestOrder> RestOrdersOfficiant { get; set; }
    }
}