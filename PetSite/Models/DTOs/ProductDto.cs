namespace PetSite.Models.DTOs
{
    public class ProductDto
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
		public bool Status { get; set; }
		public int SizeId { get; set; }
		public int ColorId { get; set; }
		public int FlavorId { get; set; }
		public int SpeciesId { get; set; }
		public int Quantity { get; set; }
		public List<IFormFile> Image { get; set; }
	}
}
