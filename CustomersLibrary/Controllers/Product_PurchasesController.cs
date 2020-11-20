using CustomersLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersLibrary.Controllers
{
    public class Product_PurchasesController : Controller
    {
        public readonly ShopContext db;
        public Product_PurchasesController(ShopContext context)
        {
            db = context;
            if (!db.Products.Any())
            {
                db.Product_Purchases.Add(new Product_Purchase { Id = 1, PurchaseId = 1 , ProductId = 1, ProductAmount = 4});
                db.Product_Purchases.Add(new Product_Purchase { Id = 1, PurchaseId = 1, ProductId = 2, ProductAmount = 4 });
                db.Product_Purchases.Add(new Product_Purchase { Id = 2, PurchaseId = 2, ProductId = 2, ProductAmount = 2 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product_Purchase>>> Get()
        {
            return await db.Product_Purchases.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product_Purchase>> Get(int id)
        {
        Product_Purchase product_purchase = await db.Product_Purchases.FirstOrDefaultAsync(x => x.PurchaseId == id);
            if (product_purchase == null)
                return NotFound();
            return new ObjectResult(product_purchase);
        }


        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Product_Purchase>> Post(Product_Purchase product_purchase)
        {
            if (product_purchase == null)
            {
                return BadRequest();
            }

            db.Product_Purchases.Add(product_purchase);
            await db.SaveChangesAsync();
            return Ok(product_purchase);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Product_Purchase>> Put(Product_Purchase product_purchase)
        {
            if (product_purchase == null)
            {
                return BadRequest();
            }
            if (!db.Product_Purchases.Any(x => x.Id == product_purchase.Id))
            {
                return NotFound();
            }

            db.Update(product_purchase);
            await db.SaveChangesAsync();
            return Ok(product_purchase);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product_Purchase>> Delete(int id)
        {
            Product_Purchase product_purchase = db.Product_Purchases.FirstOrDefault(x => x.Id == id);
            if (product_purchase == null)
            {
                return NotFound();
            }
            db.Product_Purchases.Remove(product_purchase);
            await db.SaveChangesAsync();
            return Ok(product_purchase);
        }
    }
}
