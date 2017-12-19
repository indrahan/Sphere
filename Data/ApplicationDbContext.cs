using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project5_6.Models;

namespace project5_6.Data
{
    public class WebContext : IdentityDbContext<ApplicationUser>
    {
        public WebContext(DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<project5_6.Models.Product> Product { get; set; }

        public DbSet<project5_6.Models.Category> Category { get; set; }

        public DbSet<project5_6.Models.SubCategory> SubCategory { get; set; }

        public DbSet<project5_6.Models.Laptop> Laptop { get; set; }

        public DbSet<project5_6.Models.Cart> Cart { get; set; }

        public DbSet<project5_6.Models.Wishlist> Wishlist { get; set; }

        public DbSet<project5_6.Models.ApplicationUser> ApplicationUser { get; set; }

    }
}
