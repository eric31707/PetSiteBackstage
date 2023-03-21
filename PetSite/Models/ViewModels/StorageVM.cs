namespace PetSite.Models.ViewModels
{
	public class StorageVM
	{
		public int Id { get; set; }
		public int PurchaseId { get; set; }
		public int PurchaseItemId { get; set; }
		public string ProductName { get; set; }
		public string SizeName { get; set; }
		public string ColorName { get; set; }
		public string FlavorName { get; set; }
		public decimal Price { get; set; }
		public int Qty { get; set; }
	}

}
