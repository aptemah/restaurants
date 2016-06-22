using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestOrderProduct
    {
        public int Id { get; set; }
        public virtual RestOrderPart RestOrderPart { get; set; }
        public virtual RestProduct RestProduct { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
