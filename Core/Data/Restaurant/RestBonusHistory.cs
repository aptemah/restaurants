using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum PlusMinus
    {
        Minus,
        Plus
    }
    public enum Operation
    {
        [Display(Name = "Заказ")]
        Order = 0,
        [Display(Name = "Передача бонусов")]
        Transfer = 1,
        [Display(Name = "Начисление бонусов")]
        Additional = 2,
    }
    public class RestBonusHistory
    {
        [Key]
        [ForeignKey("RestOrder")]
        public int Id { get; set; }
        public PlusMinus PlusMinus { get; set; }
        public Operation Operation { get; set; }
        public int Mount { get; set; }
        public int CurrentBonus { get; set; }
        public DateTimeOffset Date { get; set; }
        //public virtual RestAppUser RestAppUser { get; set; }
        public virtual RestOrder RestOrder { get; set; }
    }
}