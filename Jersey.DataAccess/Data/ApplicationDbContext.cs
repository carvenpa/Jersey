using Jersey.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jersey.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        //have Jersey database, define the dbset
        public DbSet<Category> Categories { get; set; }

        //have Jersey Product database, define the dbset
        public DbSet<Product> Products { get; set; }

        //have application user database, define the dbset
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //add orderHeader and OrderDetails
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        //have Jersey stores database, define the dbset
        public DbSet<Store> Stores { get; set; }

        //have shoppingcart database, define the dbset
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        //dynamically add the link of footer
        public DbSet<FooterLink> FooterLinks { get; set; }

        // insert default data into Categories and Products Table using OnModelCreating, Override the OnModelCreating method to configure your model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CatName = "English Premier League", DisplayOrder = 1},
                new Category { Id = 2, CatName = "International Team", DisplayOrder = 2},
                new Category { Id = 3, CatName = "Serie A", DisplayOrder = 3}
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductName = "England International Jersey", Size = "L", Price=459, Season="2005-2006", SleeveType='L', Description="", Edition="Fans", CategoryId=2, ImageUrl="" },
                new Product { Id = 2, ProductName = "Italy International Jersey", Size = "XL", Price = 410, Season = "2022-2023", SleeveType = 'S', Description = "The first Italian Adidas Kit", Edition="Fans", CategoryId = 2, ImageUrl="" },
                new Product { Id = 3, ProductName = "Liverpool Football Club", Size = "L", Price = 699, Season = "2025-2026", SleeveType = 'S', Description = "The 2nd Away Kit on 2025--2026", Edition="Fans", CategoryId = 1, ImageUrl = "" }
            );

            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "Kai Tak Sports Park", Address = "Shop No. M2-008, Level G, Kai Tak Mall 2, Kai Tak Sports Park, Kai Tak, Kowloon City", District = "Kowloon City District, Kowloon", PhoneNumber="(852)23218832", Description="Kai Tak Sports Park New Shop" },
                new Store { Id = 2, Name = "K11 Musea", Address = "Shop 307, 3F, K11 MUSEA, victoria Dockside, 18 Salisbury Road, Tsim Sha Tsui", District = "Yau Tsim Mong District, Kowloon", PhoneNumber = "(852)23220228", Description = "K11 Musea shop" },
                new Store { Id = 3, Name = "Hopewell Mall", Address = "Shop331, 3/F, Hopewell Mall, No.15 Kennedy Road, Wan Chai", District = "Wan Chai District, Hong Kong Island", PhoneNumber = "(852)23601123", Description = "Hopewell Mall New Shop" }
            );
        }
    }
}
