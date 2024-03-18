// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace IP_AmazonFreshIndia_Project.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<Warehouse> Warehouses { get; set; }

		// Add seeding logic here
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Seed vendors
			modelBuilder.Entity<Vendor>().HasData(
				new Vendor { VendorId = 1, Name = "Sunrise Orchid" },
				new Vendor { VendorId = 2, Name = "Mountain Fresh" }
			);

			// Seed warehouses
			modelBuilder.Entity<Warehouse>().HasData(
				new Warehouse { WarehouseId = 1, Name = "Chennai" },
				new Warehouse { WarehouseId = 2, Name = "Delhi" }
			);

			// Seed products
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					Name = "Jammu Apple",
					UnitPrice = 2.99m,
					Unit = "LB",
					VendorId = 1,
					WarehouseId = 1,
					SoldCount = 10,
					Category = "Fruits" // Category for Jammu Apple
				},
				new Product
				{
					ProductId = 2,
					Name = "Himalayan Honey",
					UnitPrice = 5.49m,
					Unit = "Box",
					VendorId = 2,
					WarehouseId = 2,
					SoldCount = 15,
					Category = "Grocery" // Category for Himalayan Honey
				},
				new Product
				{
					ProductId = 3,
					Name = "Kashmiri Saffron",
					UnitPrice = 25.99m,
					Unit = "Gram",
					VendorId = 1,
					WarehouseId = 1,
					SoldCount = 20,
					Category = "Spices" // Category for Kashmiri Saffron
				}
			);
		}
	}
}
