using Microsoft.AspNetCore.Mvc;
using IP_AmazonFreshIndia_Project.ViewModels;
using IP_AmazonFreshIndia_Project.Models;
using IP_AmazonFreshIndia_Project.Data;
using System.Linq;


namespace IP_AmazonFreshIndia_Project.Controllers
{
	// [Area("Admin")]
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: /Admin/Product
		public IActionResult Index(string category, string warehouse, string vendor)
		{
			// Your logic to retrieve products based on filters
			IQueryable<Product> productsQuery = _context.Products;

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
			var viewModel = new List<ProductViewModel>();

			foreach (var product in products)
			{
				viewModel.Add(new ProductViewModel
				{
					ProductId = product.ProductId,
					Name = product.Name,
					UnitPrice = product.UnitPrice,
					Unit = product.Unit,
					VendorName = product.Vendor.Name,
					WarehouseName = product.Warehouse.Name,
					SoldCount = product.SoldCount
				});
			}

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

		// POST: /Admin/Product/Update
		[HttpPost]
		public IActionResult Update(ProductViewModel productViewModel)
		{
			if (ModelState.IsValid)
			{
				// Your logic to update product in database
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
			return View("Edit", productViewModel);
		}

		// POST: /Admin/Product/Delete
		[HttpPost]
		public IActionResult Delete(int productId)
		{
			var product = _context.Products.Find(productId);
			if (product != null)
			{
				// Your logic to delete product from database
				_context.Products.Remove(product);
				_context.SaveChanges();
				TempData["Message"] = "Product deleted successfully";
			}
			return RedirectToAction("Index");
		}
	}
}
