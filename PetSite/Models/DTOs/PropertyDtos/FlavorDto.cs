using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.PropertyDtos
{
	public class FlavorDto
	{
		public int FlavorId { get; set; }
		public string Name { get; set; }
	}
	public static partial class FlavorExts
	{
		public static FlavorDto ToFlavorDto(this Flavor source)
			=> new FlavorDto
			{
				FlavorId = source.FlavorId,
				Name = source.Name,
			};
	}
}
