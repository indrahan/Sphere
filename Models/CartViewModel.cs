using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;

namespace project5_6.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int order_no { get; set; }
        public string user_id { get; set; }
        public int product_id { get; set; }
        public string brand { get; set; }
        public string model_name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

    }
    public class ViewModel
    {
        public Cart Cart { get; set; }
        public Product[] Product { get; set; }
    }
}