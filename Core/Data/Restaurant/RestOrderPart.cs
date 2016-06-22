using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum TypeOfOrder
    {
        [Display(Name = "в ресторане")]
        Restaurant = 0,
        [Display(Name = "блюдо с собой")]
        Self = 1,
        [Display(Name = "доставка по адресу")]
        Address = 2,
    }
    public enum ValidPurchase
    {
        [Display(Name = "заказ не подтвержден")]
        NotConfirmed = 0,
        [Display(Name = "заказ подтвержден")]
        Confirmed = 1
    }
    public class RestOrderPart
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Comment { get; set; }
        public string CookTime { get; set; }
        public ValidPurchase ValidPurchase { get; set; }
        public TypeOfOrder TypeOfOrder { get; set; }
        public virtual RestOrder RestOrder { get; set; }
        public virtual ICollection<RestOrderProduct> RestOrderProducts { get; set; }
    }
}