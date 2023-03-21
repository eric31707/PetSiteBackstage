using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.PropertyDtos
{
	public class SpeciesDto
	{
		public int SpeciesId { get; set; }
		public string Name { get; set; }
	}
	public static partial class SpeciesExts
	{
		public static SpeciesDto ToSpeciesDto(this Species source)
			=> new SpeciesDto
			{
				SpeciesId = source.SpeciesId,
				Name = source.Name,
			};
	}
}
