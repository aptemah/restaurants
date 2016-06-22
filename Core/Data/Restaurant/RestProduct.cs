using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public class RestProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Activity Activity { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public RestCategory Category { get; set; }
        //public virtual RestCategory Category { get; set; }
        public virtual ICollection<RestProductFavorite> RestProductFavorites { get; set; }
        public virtual ICollection<RestOrderProduct> RestOrderProducts { get; set; }
        public virtual ICollection<RestProdReview> RestProdReviews { get; set; }
    }
}