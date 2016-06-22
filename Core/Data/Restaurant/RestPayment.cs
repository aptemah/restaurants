using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestPayment
    {
        [Key]
        [ForeignKey("RestAppUser")]
        public int Id { get; set; }
        public string Token { get; set; }
        public int Bonus { get; set; }
        public virtual RestAppUser RestAppUser { get; set; }
    }
}