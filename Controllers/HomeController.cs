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