using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project5_6.Data;
using project5_6.Models;
using Microsoft.AspNetCore.Authorization;


namespace project5_6.Controllers
{
    public class WishlistController : Controller
    {
        private readonly WebContext _context;

        public WishlistController(WebContext context)
        {
            _context = context;
        }

        // GET: Wishlist
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wishlist.ToListAsync());
        }

        public async Task<IActionResult> Index2(string id)
        {
            var webContext = _context.Wishlist.Where(p => p.user_id.Equals(id));
            ViewBag.TotalPrice = _context.Wishlist.Where(c => c.user_id == id).Sum(c => c.price);
            return View(await webContext.ToListAsync());
        }


        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var wishlist = await _context.Wishlist
                .SingleOrDefaultAsync(m => m.Id == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // GET: Wishlist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,user_id,product_id,brand,model_name,description,price")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wishlist);
        }

        // GET: Wishlist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist.SingleOrDefaultAsync(m => m.Id == id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return View(wishlist);
        }

        public ActionResult Create2(int image_id, string brand, string model_name, int order_no, string user_id, int quantity, int price)
        {
            Random random = new Random();
            _context.Cart.Add(new Cart() { product_id = image_id, brand = brand, model_name = model_name, order_no = random.Next(0, 55555), user_id = user_id, quantity = 1, price = price });
            _context.SaveChanges();

            //Return a view of whatever now
            return RedirectToAction("Index3", "Cart", new { id = user_id });

        }
        public async Task<IActionResult> Delete2(int id, string user_id)
        {
            var wishlist = await _context.Wishlist.SingleOrDefaultAsync(m => m.Id == id);
            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index2", "Wishlist", new { id = user_id });
        }

        // POST: Wishlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,user_id,product_id,brand,model_name,description,price")] Wishlist wishlist)
        {
            if (id != wishlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistExists(wishlist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wishlist);
        }

        // GET: Wishlist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist
                .SingleOrDefaultAsync(m => m.Id == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // POST: Wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wishlist = await _context.Wishlist.SingleOrDefaultAsync(m => m.Id == id);
            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(int id)
        {
            return _context.Wishlist.Any(e => e.Id == id);
        }
    }
}
