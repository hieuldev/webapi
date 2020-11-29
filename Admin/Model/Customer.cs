using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    class Customer
    {
        public int CustomerID { get; set; }

        public string CustomerUsername { get; set; }

   
        public string CustomerEmail { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }
 
        public string CustomerAdress { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
