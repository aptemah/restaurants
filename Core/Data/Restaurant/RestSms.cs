using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum UsedSms
    {
        [Display(Name = "не использован")]
        NotUsed = 0,
        [Display(Name = "использован")]
        Used = 1,
    }
    public class RestSms
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Phone { get; set; }
        public UsedSms UsedSms { get; set; }
        public DateTime Date { get; set; }
        public virtual RestAppUser RestAppUser { get; set; }
    }
}