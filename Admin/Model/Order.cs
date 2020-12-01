using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }
        public DateTime? OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal? Total { get; set; }


    }
}
