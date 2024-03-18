// In ProductFilterViewModel.cs
using System.Collections;
using System.Collections.Generic;

namespace IP_AmazonFreshIndia_Project.ViewModels
{
	public class ProductFilterViewModel : IEnumerable<ProductViewModel>
	{
		public string Category { get; set; }
		public string Warehouse { get; set; }
		public string Vendor { get; set; }


		public List<string> Categories { get; set; }
		public List<string> Warehouses { get; set; }
		public List<string> Vendors { get; set; }
		public string SelectedCategory { get; set; }
		public string SelectedWarehouse { get; set; }
		public string SelectedVendor { get; set; }

		public List<ProductViewModel> Products { get; set; }

		public IEnumerator<ProductViewModel> GetEnumerator()
		{
			return Products.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
