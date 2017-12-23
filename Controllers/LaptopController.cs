using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project5_6.Data;
using project5_6.Models;


namespace project5_6.Controllers
{
    public class LaptopController : Controller
    {
        private readonly WebContext _context;

        public LaptopController(WebContext context)
        {
            _context = context;
        }

        public IQueryable<Laptop> FilterContext(WebContext _currentcontext, string _subject, string _filter)
        {
            IQueryable<Laptop> NewContext = _currentcontext.Laptop;
            if (_subject == "brand")
            {
                NewContext = _currentcontext.Laptop.Where(p => p.brand.Contains(_filter));
            }
            else if (_subject == "processor")
            {
                NewContext = _currentcontext.Laptop.Where(p => p.processor.Contains(_filter));
            }
            else if (_subject == "screen_size")
            {
                NewContext = _currentcontext.Laptop.Where(p => p.screen_size.Contains(_filter));
            }
            else if (_subject == "purpose")
            {
                NewContext = _currentcontext.Laptop.Where(p => p.recommended_purpose.Contains(_filter));
            }
            else if (_subject == "ram")
            {
                NewContext = _currentcontext.Laptop.Where(p => p.ram.Contains(_filter));
            }
            else if (_subject == "storage_size")
            {
                NewContext = _currentcontext.Laptop.Where(p => p.main_storage.Contains(_filter));
            }
            return NewContext;
        }

        // GET: Laptop
        public async Task<IActionResult> Index(string processor, string brand, string recommended_purpose, string screen_size, string ram, string main_storage, bool hdmi)
        {
            ViewBag.laptop = (from x in _context.Laptop.OrderBy(x => x.brand) select x.brand).Distinct();
            ViewBag.processor = (from x in _context.Laptop.OrderBy(x => x.processor)select x.processor).Distinct();
            ViewBag.screen_size = (from x in _context.Laptop.OrderBy(x => x.screen_size) select x.screen_size  ).Distinct();
            ViewBag.recommended_purpose = (from x in _context.Laptop.OrderBy(x => x.recommended_purpose) select x.recommended_purpose).Distinct();
            ViewBag.ram = (from x in _context.Laptop.OrderByDescending(x => x.ram) select x.ram).Distinct();
            ViewBag.main_storage = (from x in _context.Laptop.OrderByDescending(x => x.main_storage) select x.main_storage).Distinct();

            IQueryable<Laptop> MyContext = _context.Laptop.OrderByDescending(x => x.supply);

            if (brand != null)
            {
                MyContext = MyContext.Where(p => p.brand.Contains(brand));
            }
            if (processor != null)
            {
                MyContext = MyContext.Where(p => p.processor.Contains(processor));
            }
            if (screen_size != null)
            {
                MyContext = MyContext.Where(p => p.screen_size.Contains(screen_size));
            }
            if (recommended_purpose != null)
            {
                MyContext = MyContext.Where(p => p.recommended_purpose.Contains(recommended_purpose));
            }
            if (ram != null)
            {
                MyContext = MyContext.Where(p => p.ram.Contains(ram));
            }
            if (main_storage != null)
            {
                MyContext = MyContext.Where(p => p.main_storage.Contains(main_storage));
            }

            return View(await MyContext.ToListAsync());
        }

        public async Task<IActionResult> Index2(int id)
        {
            var webContext = _context.Laptop.Where(p => p.Id.Equals(id));

            return View(await webContext.ToListAsync());
        }

        // GET: Laptop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptop
                .SingleOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _context.Laptop.ToListAsync());
        }


        // GET: Laptop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laptop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,date_added,image_id,category,brand,model_name,description,price,screen_size,panel_type,keyboard_layout,operating_system,processor,graphic_card,ram,main_storage,main_storage_type,extra_storage,webcam,hdmi,touchscreen,dvd_drive,staff_picked,recommended_purpose")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laptop);
        }
        public ActionResult Create2(int image_id, string brand, string model_name, int order_no, string user_id, int quantity, int price)
        {
            Random random = new Random();
            _context.Cart.Add(new Cart() { product_id = image_id, brand = brand, model_name = model_name, order_no = random.Next(0, 55555), user_id = user_id, quantity = 1, price = price });
            _context.SaveChanges();

            //Return a view of whatever now
            return RedirectToAction("Index3", "Cart", new { id = user_id });

        }

        public ActionResult Create3(int image_id, string brand, string model_name, string user_id, int price)
        {
            Random random = new Random();
            _context.Wishlist.Add(new Wishlist() { product_id = image_id, brand = brand, model_name = model_name, user_id = user_id, price = price });
            _context.SaveChanges();

            //Return a view of whatever now
            return RedirectToAction("Index2", "Wishlist", new { id = user_id });

        }

        // GET: Laptop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptop.SingleOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }
            return View(laptop);
        }

        // POST: Laptop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,date_added,image_id,category,brand,model_name,description,price,screen_size,panel_type,keyboard_layout,operating_system,processor,graphic_card,ram,main_storage,main_storage_type,extra_storage,webcam,hdmi,touchscreen,dvd_drive,staff_picked,recommended_purpose")] Laptop laptop)
        {
            if (id != laptop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopExists(laptop.Id))
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
            return View(laptop);
        }

        // GET: Laptop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptop
                .SingleOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // POST: Laptop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptop = await _context.Laptop.SingleOrDefaultAsync(m => m.Id == id);
            _context.Laptop.Remove(laptop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopExists(int id)
        {
            return _context.Laptop.Any(e => e.Id == id);
        }
    }
}
