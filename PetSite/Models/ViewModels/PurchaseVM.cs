namespace PetSite.Models.ViewModels
{
	public class PurchaseVM
	{
		public int Id { get; set; }
		public int SupplierId { get; set; }
		public bool Status { get; set; }
		public decimal Total { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
