namespace PetSite.Models.DTOs
{
	public class PurchaseCreateDto
	{
		public int PurchaseId { get; set; }
		public int SupplierId { get; set; }
		public bool Status { get; set; }
		public decimal Total { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
