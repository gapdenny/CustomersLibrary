using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CustomersLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        readonly ShopContext db;
        public CustomersController(ShopContext context)
        {
            db = context;
            if (!db.Customers.Any())
            {
                db.Customers.Add(new Customer { FullName = "Tom", Age = 26 });
                db.Customers.Add(new Customer { FullName = "Alice", Age = 31 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await db.Customers.ToListAsync();
        }

        // GET api/customerss/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            Customer user = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
        // GET api/customerss/USA
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByCountry(string Country)
        {
            Customer user = await db.Customers.FirstOrDefaultAsync(x => x.Country == Country);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostAddCustomer(Customer user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            MD5 Cipher = MD5.Create();
            user.Password = Encoding.ASCII.GetString(Cipher.ComputeHash(Encoding.ASCII.GetBytes(user.Password)));
            db.Customers.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/customers/
        [HttpPut]
        public async Task<ActionResult<Customer>> Put(Customer user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Customers.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            Customer user = db.Customers.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Customers.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

    }
}
