using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.IO;

namespace project5_6.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string ProductBrand { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int SubCategoryId{get; set;}
        public SubCategory SubCategory { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }
    public class SubCategory
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId {get; set;}
        public List<Product> Products { get; set; }
    }
}