using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project5_6.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using project5_6.Data;

namespace project5_6.Controllers
{
    public class AdminController : Controller
    {

        private readonly WebContext _context;
        public AdminController(WebContext context)
        {
            _context = context;

        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            ViewData["Message"] = "This a basic admin page";
            return View();
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            System.Console.WriteLine("test");

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    if (formFile.FileName.EndsWith(".csv"))
                    {


                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var col = line.Split(',');
                            var laptop = new Laptop()
                            {
                                product_id = int.Parse(col[0]),
                                date_added = DateTime.Parse(col[1]),
                                image_id = int.Parse(col[2]),
                                category = col[3],
                                brand = col[4],
                                model_name = col[5],
                                price = float.Parse(col[6]),
                                screen_size = (col[7]),
                                panel_type = col[8],
                                operating_system = col[9],
                                processor = col[10],
                                graphic_card = col[11],
                                ram = col[12],
                                main_storage = col[13],
                                extra_storage = Boolean.Parse(col[14]),
                                webcam = Boolean.Parse(col[15]),
                                hdmi = Boolean.Parse(col[16]),
                                touchscreen = Boolean.Parse(col[17]),
                                dvd_drive = Boolean.Parse(col[18]),
                                staff_picked = Boolean.Parse(col[19]),
                                recommended_purpose = col[20],
                                supply = int.Parse(col[21]),
                                description = col[22]
                            };

                            _context.Laptop.Add(laptop);
                        }

                        _context.SaveChanges();
                    }
                }
            }


            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        
        public IActionResult Statistics()
        {
<<<<<<< HEAD
            ViewBag.january = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 01, 01) && x.date_added < new DateTime(2017, 02, 01) select x.supply).Sum();
            ViewBag.february = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 02, 01) && x.date_added < new DateTime(2017, 03, 01) select x.supply).Sum();
            ViewBag.march = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 03, 01) && x.date_added < new DateTime(2017, 04, 01) select x.supply).Sum();
            ViewBag.april = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 04, 01) && x.date_added < new DateTime(2017, 05, 01) select x.supply).Sum();
            ViewBag.may = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 05, 01) && x.date_added < new DateTime(2017, 06, 01) select x.supply).Sum();
            ViewBag.june = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 06, 01) && x.date_added < new DateTime(2017, 07, 01) select x.supply).Sum();
            ViewBag.july = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 07, 01) && x.date_added < new DateTime(2017, 08, 01) select x.supply).Sum();
            ViewBag.august = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 08, 01) && x.date_added < new DateTime(2017, 09, 01) select x.supply).Sum();
            ViewBag.september = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 09, 01) && x.date_added < new DateTime(2017, 10, 01) select x.supply).Sum();
            ViewBag.october = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 10, 01) && x.date_added < new DateTime(2017, 11, 01) select x.supply).Sum();
            ViewBag.november = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 11, 01) && x.date_added < new DateTime(2017, 12, 01) select x.supply).Sum();
            ViewBag.december = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 12, 01) && x.date_added < new DateTime(2018, 01, 01) select x.supply).Sum();

=======
            ViewBag.OutOfStockCount = (from x in _context.Laptop where x.supply <= 2 select x.Id).Count();
            return View();
        }
        
        [Route("Admin/Statistics/Products/OutOfStock")]
        public IActionResult ProductsOutOfStock()
        {
            var webContext = _context.Laptop.Where(p => p.supply <= 2).OrderBy(p => p.supply);
            return View(webContext.ToList());
        }

        [Route("Admin/Statistics/Products/ProductsAdded/{year:regex(\\d{{4}}):range(2000,2999)}")]
        public IActionResult ProductsAdded(int year)
        {
            int[] GetAmountAddedByMonth(int _year)
            {
                int[] AmountAddedByMonth = new int[12];
                for (int i = 0; i < AmountAddedByMonth.Length; i++)
                {
                    if (i == AmountAddedByMonth.Length - 1)
                    {
                        AmountAddedByMonth[i] = (from x in _context.Laptop where x.date_added >= new DateTime(_year, (i + 1), 1) && x.date_added < new DateTime((_year + 1), 1, 1) select x.supply).Sum();
                    }
                    else
                    {
                        AmountAddedByMonth[i] = (from x in _context.Laptop where x.date_added >= new DateTime(_year, (i + 1), 1) && x.date_added < new DateTime(_year, (i + 2), 1) select x.supply).Sum();
                    }
                }
                return AmountAddedByMonth;

            }
            ViewBag.AmountAdded = GetAmountAddedByMonth(year);
            //ViewBag.january = (from x in _context.Laptop where x.date_added >= new DateTime(2017, 01, 01) && x.date_added < new DateTime(2017, 02, 1) select x.supply).Sum();
>>>>>>> master
            return View();
        }
    }
}
