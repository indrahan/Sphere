using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project5_6.Models;
using project5_6.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace project5_6.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebContext _context;

        public HomeController(WebContext context)
        {
            _context = context;
            // insertdata();
        }
        public void insertdata()
        {
            var file = "HP.csv";
            //StreamReader = for reading lines of information from a standard text file
            var sr = new StreamReader(file);
            //Exit the loop when done reading a file
            while (!sr.EndOfStream)
            {
                //Readline = for reading data from a readable stream one line at a time.
                var line = sr.ReadLine();
                //Split = to split or breakup a string and add the data to a string array using a defined separator. 
                //If no separator is defined when you call upon the function, whitespace will be used by default.
                var col = line.Split(',');
                //The data seperated by a ',' are put in a column; "col".
                //The columns are then assigned to the corresponding attribues.
                //Counting columns starts from 0
                //ex: col[0] = 3, ===> image_id = 3
                var laptop = new Laptop() { date_added = DateTime.Now, image_id = int.Parse(col[1]), category = col[2], brand = col[3], model_name = col[4], description = col[5], price = float.Parse(col[6]), screen_size = float.Parse(col[7]), panel_type = col[8], keyboard_layout = col[9], operating_system = col[10], processor = col[11], graphic_card = col[12], ram = int.Parse(col[13]), main_storage = int.Parse(col[14]), main_storage_type = col[15], extra_storage = Boolean.Parse(col[16]), webcam = Boolean.Parse(col[17]), hdmi = Boolean.Parse(col[18]), touchscreen = Boolean.Parse(col[19]), dvd_drive = Boolean.Parse(col[20]), staff_picked = Boolean.Parse(col[21]), recommended_purpose = col[22] };
                _context.Laptop.Add(laptop);

            }
            _context.SaveChanges();
        }
        public async Task<IActionResult> Index(string searchString, string searchString2)
        {
            var webContext = _context.Laptop.OrderByDescending(p => p.date_added).Take(6);
            var webContext2 = _context.Laptop.Where(p => p.brand.ToLower().Contains(searchString.ToLower()));

            if (searchString == null)
            {
                return View(await webContext.ToListAsync());
            }
            else
            {
                return View(await webContext2.ToListAsync());

            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
