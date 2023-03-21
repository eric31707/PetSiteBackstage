using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.PropertyDtos
{
	public class SizeDto
	{
		public int SizeId { get; set; }
		public string Name { get; set; }
	}
	public static partial class SizeExts
	{
		public static SizeDto ToSizeDto(this Size source)
			=> new SizeDto
			{
				SizeId = source.SizeId,
				Name = source.Name,
			};
	}
}
