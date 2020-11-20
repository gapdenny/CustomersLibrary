using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomersLibrary.Models;

namespace CustomersLibrary.Controllers
{
    public class PurchasesController : Controller
    {

        readonly ShopContext db;
        public PurchasesController(ShopContext context)
        {
            db = context;
            if (!db.Purchases.Any())
            {
                db.Purchases.Add(new Purchase { CustomerId = 1, CreateTime = DateTime.Parse("Jan 1, 2019"), PurchaseId = 1});
                db.Purchases.Add(new Purchase { CustomerId = 1, CreateTime = DateTime.Parse("Jan 6, 2019"), PurchaseId = 2 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> Get()
        {
            return await db.Purchases.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> Get(int id)
        {
            Purchase purchase = await db.Purchases.FirstOrDefaultAsync(x => x.PurchaseId == id);
            if (purchase == null)
                return NotFound();
            return new ObjectResult(purchase);
        }


        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Purchase>> Post(Purchase purchase)
        {
            if (purchase == null)
            {
                return BadRequest();
            }

            db.Purchases.Add(purchase);
            foreach (var item in purchase.LineItems){

                Product_Purchase Toinser = new Product_Purchase { ProductId = item.ProductId, PurchaseId = purchase.PurchaseId };
                db.Product_Purchases.Add(Toinser); 
            }
            await db.SaveChangesAsync();
            return Ok(purchase);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Purchase>> Put(Purchase purchase)
        {
            if (purchase == null)
            {
                return BadRequest();
            }
            if (!db.Purchases.Any(x => x.PurchaseId == purchase.PurchaseId))
            {
                return NotFound();
            }

            db.Update(purchase);
            await db.SaveChangesAsync();
            return Ok(purchase);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Purchase>> Delete(int id)
        {
            Purchase purchase = db.Purchases.FirstOrDefault(x => x.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }
            db.Purchases.Remove(purchase);
            await db.SaveChangesAsync();
            return Ok(purchase);
        }
    }
}
