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
    public class CartController : Controller
    {
        private readonly WebContext _context;

        public CartController(WebContext context)
        {
            _context = context;
        }

        // GET: Cart
        [Authorize(Roles = "Registered")]
        public async Task<IActionResult> Index(string id)
        {
            var webContext = _context.Cart.Where(p => p.user_id.Equals(id));

            return View(await webContext.ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            return View(await _context.Cart.ToListAsync());
        }

        public async Task<IActionResult> Index3(string id)
        {
            var webContext = _context.Cart.Where(p => p.user_id.Equals(id));
            ViewBag.TotalPrice = _context.Cart.Where(c => c.user_id == id).Sum(c => c.price);
            return View(await webContext.ToListAsync());
        }

        public async Task<IActionResult> WishList(string id)
        {
            var webContext = _context.Cart.Where(p => p.user_id.Equals(id));
            return View(await webContext.ToListAsync());
        }
        public async Task<IActionResult> Checkout(string id)
        {
            var webContext = _context.Cart.Where(p => p.user_id.Equals(id));

            return View(await webContext.ToListAsync());
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Add(int id)
        {
            var webContext = _context.Cart.Where(p => p.product_id.Equals(id));

            return View(id);
        }
        public async Task<IActionResult> Delete3(int id, string user_id)
        {
            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index3", "Cart", new { id = user_id });
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,order_no,user_id,product_id,quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Id,order_no,user_id,product_id,quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }
        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,order_no,user_id,product_id,quantity")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .SingleOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
