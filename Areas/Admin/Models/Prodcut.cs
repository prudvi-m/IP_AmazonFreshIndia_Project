public class Product
{
	public int ProductId { get; set; }
	public string Name { get; set; }
	public decimal UnitPrice { get; set; }
	public string Unit { get; set; }
	public int VendorId { get; set; }
	public Vendor Vendor { get; set; }
	public int WarehouseId { get; set; }
	public Warehouse Warehouse { get; set; }
	public int SoldCount { get; set; }
}
