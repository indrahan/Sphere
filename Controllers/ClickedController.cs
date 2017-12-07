using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project5_6.Data;
using project5_6.Models;

namespace project5_6.Controllers
{
    public class ClickedController : Controller
    {
        private readonly WebContext _context;

        public ClickedController(WebContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var webContext = _context.Product.Where(p => p.Id.Equals(id));

            return View(await webContext.ToListAsync());
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
    }
}
