using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersLibrary.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public int Age { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public virtual ICollection<Purchase> Purchases{ get; set; }
    }
}
