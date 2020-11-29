using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    class Product
    {
        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal? PromotionPrice { get; set; }

        public string ShowImage_1 { get; set; }

        public string ShowImage_2 { get; set; }

        public int? ProductStock { get; set; }

        public bool? ProductStatus { get; set; }

        public int? ViewCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CategoryID { get; set; }

    }
}
