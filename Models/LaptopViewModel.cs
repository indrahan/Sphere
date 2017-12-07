using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;

namespace project5_6.Models
{
    public class Laptop
    {
        public int Id { get; set; }
        public DateTime date_added { get; set; }
        public int image_id { get; set; }
        public string category { get; set; }
        public string brand { get; set; }
        public string model_name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public float screen_size { get; set; }  
        public string panel_type { get; set; }  
        public string keyboard_layout { get; set; } 
        public string operating_system { get; set; }
        public string processor { get; set; }
        public string graphic_card { get; set; }
        public int ram { get; set; }
        public int main_storage { get; set; }
        public string main_storage_type { get; set; }
        public bool extra_storage { get; set; }
        public bool webcam { get; set; }
        public bool hdmi { get; set; }
        public bool touchscreen { get; set; }
        public bool dvd_drive { get; set; }
        public bool staff_picked { get; set; }
        public string recommended_purpose { get; set; }

    }
}