using Microsoft.AspNetCore.Mvc;
using IP_AmazonFreshIndia_Project.ViewModels;
using IP_AmazonFreshIndia_Project.Models;
using IP_AmazonFreshIndia_Project.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IP_AmazonFreshIndia_Project.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductController(ApplicationDbContext context)
		{
			_context = context;
		}

		// In ProductController.cs
		public IActionResult Index(string category, string warehouse, string vendor)
		{
			IQueryable<Product> productsQuery = _context.Products.Include(p => p.Vendor).Include(p => p.Warehouse);

			// Apply filters if provided
			if (!string.IsNullOrEmpty(category))
			{
				productsQuery = productsQuery.Where(p => p.Category == category);
			}
			if (!string.IsNullOrEmpty(warehouse))
			{
				productsQuery = productsQuery.Where(p => p.Warehouse.Name == warehouse);
			}
			if (!string.IsNullOrEmpty(vendor))
			{
				productsQuery = productsQuery.Where(p => p.Vendor.Name == vendor);
			}

			var products = productsQuery.ToList();
			var productViewModels = products.Select(p => new ProductViewModel
			{
				ProductId = p.ProductId,
				Name = p.Name,
				UnitPrice = p.UnitPrice,
				Unit = p.Unit,
				VendorName = p.Vendor.Name,
				WarehouseName = p.Warehouse.Name,
				SoldCount = p.SoldCount
			}).ToList();

			var viewModel = new ProductFilterViewModel
			{
				Category = category,
				Warehouse = warehouse,
				Vendor = vendor,
				Products = productViewModels
			};

			// Get distinct categories
			viewModel.Categories = _context.Products.Select(p => p.Category).Distinct().ToList();
			// Get distinct warehouses
			viewModel.Warehouses = _context.Warehouses.Select(w => w.Name).Distinct().ToList();
			// Get distinct vendors
			viewModel.Vendors = _context.Vendors.Select(v => v.Name).Distinct().ToList();

			// Set selected values
			viewModel.SelectedCategory = category;
			viewModel.SelectedWarehouse = warehouse;
			viewModel.SelectedVendor = vendor;

			return View(viewModel);
		}
		// POST: /Admin/Product/Add
		[HttpPost]
		public IActionResult Add(ProductViewModel productViewModel)
		{
			if (ModelState.IsValid)
			{
				// Your logic to add product to database
				_context.Products.Add(new Product
				{
					Name = productViewModel.Name,
					// Assign other properties
				});
				_context.SaveChanges();

				TempData["Message"] = "Product added successfully";
				return RedirectToAction("Index");
			}

			// If ModelState is not valid, return back to the form with validation errors
			return View("Create", productViewModel);
		}

		// GET: /Admin/Product/Create
		public IActionResult Create()
		{
			return View();
		}

		// GET: /Admin/Product/Edit/{id}
		public IActionResult Edit(int id)
		{
			var product = _context.Products.Include(p => p.Vendor).Include(p => p.Warehouse).FirstOrDefault(p => p.ProductId == id);
			if (product == null)
			{
				return NotFound(); // Return 404 if product not found
			}

			var productViewModel = new ProductViewModel
			{
				ProductId = product.ProductId,
				Name = product.Name,
				UnitPrice = product.UnitPrice,
				Unit = product.Unit,
				VendorName = product.Vendor.Name,
				WarehouseName = product.Warehouse.Name,
				SoldCount = product.SoldCount
			};

			return View(productViewModel);
		}

		// POST: /Admin/Product/Edit
		[HttpPost]
		public IActionResult Edit(ProductViewModel productViewModel)
		{
			if (ModelState.IsValid)
			{
				var product = _context.Products.Find(productViewModel.ProductId);
				if (product != null)
				{
					product.Name = productViewModel.Name;
					// Update other properties
					_context.SaveChanges();
					TempData["Message"] = "Product updated successfully";
					return RedirectToAction("Index");
				}
			}

			// If ModelState is not valid or product not found, return back to the form with validation errors
			return View(productViewModel);
		}

		// POST: /Admin/Product/Delete
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var product = _context.Products.Find(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				_context.SaveChanges();
				TempData["Message"] = "Product deleted successfully";
			}
			return RedirectToAction("Index");
		}



	}
}
