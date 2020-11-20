using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersLibrary.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public DateTime CreateTime { get; set; }

        public int CustomerId { get; set; }

        public decimal Amount { get; set; }
      
        public IList<Product> LineItems { get; set; }
    }
}
