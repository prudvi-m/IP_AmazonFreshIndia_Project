﻿namespace IP_AmazonFreshIndia_Project.Models
{
	public class ProductCategory
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }

		public Category Category { get; set; }
		public Product Product { get; set; }
	}
}
