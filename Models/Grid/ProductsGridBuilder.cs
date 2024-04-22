using Microsoft.AspNetCore.Http;

namespace IP_AmazonFreshIndia_Project.Models
{
	public class ProductsGridBuilder : GridBuilder
	{
		public ProductsGridBuilder(ISession sess) : base(sess) { }

		public ProductsGridBuilder(ISession sess, ProductsGridDTO values,
			string defaultSortField) : base(sess, values, defaultSortField)
		{
			bool isInitial = values.Warehouse.IndexOf(FilterPrefix.Warehouse) == -1;
			routes.CategoryFilter = (isInitial) ? FilterPrefix.Category + values.Category : values.Category;
			routes.WarehouseFilter = (isInitial) ? FilterPrefix.Warehouse + values.Warehouse : values.Warehouse;
			routes.VendorFilter = (isInitial) ? FilterPrefix.Vendor + values.Vendor : values.Vendor;
			routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
		}

		public void LoadFilterSegments(string[] filter, Category category)
		{
			if (category == null)
			{
				routes.CategoryFilter = FilterPrefix.Category + filter[0];
			}
			else
			{
				routes.CategoryFilter = FilterPrefix.Category + filter[0]
					+ "-" + category.Name.Slug();
			}
			routes.WarehouseFilter = FilterPrefix.Warehouse + filter[1];
			routes.PriceFilter = FilterPrefix.Price + filter[2];
			routes.VendorFilter = FilterPrefix.Vendor + filter[2];
		}

		public void ClearFilterSegments() => routes.ClearFilters();

		string def = ProductsGridDTO.DefaultFilter;
		public bool IsFilterByCategory => routes.CategoryFilter != def;
		public bool IsFilterByWarehouse => routes.WarehouseFilter != def;

		public bool IsFilterByVendor => routes.VendorFilter != def;
		public bool IsFilterByPrice => routes.PriceFilter != def;

		public bool IsSortByWarehouse =>
			routes.SortField.EqualsNoCase(nameof(Warehouse));
			public bool IsSortByVendor =>
				routes.SortField.EqualsNoCase(nameof(Product.Vendor));
		public bool IsSortByPrice =>
			routes.SortField.EqualsNoCase(nameof(Product.Price));
	}
}
