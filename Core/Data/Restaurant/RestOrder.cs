using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum OpenClose
    {
        [Display(Name = "Счет открыт")]
        Open = 0,
        [Display(Name = "Счет закрыт")]
        Close = 1
    }
    public enum TypeOfPayment
    {
        [Display(Name = "Оплата наличными")]
        Cash = 0,
        [Display(Name = "Оплата кредитной картой")]
        CreditCard = 1,
        [Display(Name = "Оплата бонусами")]
        Bonus = 2
    }
    public enum Tip
    {
        [Display(Name = "0%")]
        Zero = 0,
        [Display(Name = "2%")]
        Two = 1,
        [Display(Name = "5%")]
        Five = 2,
        [Display(Name = "10%")]
        Ten = 3,
        [Display(Name = "15%")]
        Fifteen = 4,
        [Display(Name = "20%")]
        Twenty = 5
    }
    
    public class RestOrder
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreate { get; set; }
        public DateTimeOffset DateClose { get; set; }
        public OpenClose OpenClose { get; set; }
        public TypeOfPayment? TypeOfPayment { get; set; }
        public Tip Tip { get; set; }
        public int BonusThisTime { get; set; }
        public string TableNumber { get; set; }
        public string OrderNumber { get; set; }
        public string OrderAddress { get; set; }
        public string WaiterComment { get; set; }
        public virtual RestAppUser Officiant { get; set; }
        public virtual ICollection<RestOrderPart> RestOrderParts { get; set; }
        public virtual RestPoint RestPoint { get; set; }
        public virtual RestAppUser RestAppUser { get; set; }
        public virtual RestBonusHistory RestBonusHistory { get; set; }
    }
}