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
                    System.Console.WriteLine("File : " + formFile.FileName);
                    if (formFile.FileName.EndsWith(".csv"))
                    {
                        System.Console.WriteLine("HUE");

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        var sr = new StreamReader(formFile.OpenReadStream());
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var col = line.Split(',');
                            var laptop = new Laptop() { date_added = DateTime.Parse(col[0]), image_id = int.Parse(col[1]), category = col[2], brand = col[3], model_name = col[4], description = col[5], price = float.Parse(col[6]), screen_size = col[7], panel_type = col[8], keyboard_layout = col[9], operating_system = col[10], processor = col[11], graphic_card = col[12], ram = col[13], main_storage = col[14], main_storage_type = col[15], extra_storage = Boolean.Parse(col[16]), webcam = Boolean.Parse(col[17]), hdmi = Boolean.Parse(col[18]), touchscreen = Boolean.Parse(col[19]), dvd_drive = Boolean.Parse(col[20]), staff_picked = Boolean.Parse(col[21]), recommended_purpose = col[22] };
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
    }
}
