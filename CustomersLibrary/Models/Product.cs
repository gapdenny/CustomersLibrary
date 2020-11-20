using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersLibrary.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string Description { get; set; }
    }
}
