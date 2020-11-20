using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersLibrary.Models
{
    public class Product_Purchase
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
    
        public int ProductAmount { get; set; }

        public int PurchaseId { get; set; }
    }
}
