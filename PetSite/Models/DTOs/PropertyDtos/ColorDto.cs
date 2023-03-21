using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.PropertiesDtos
{
	public class ColorDto
	{
		public int ColorId { get; set; }
		public string Name { get; set; }
	}
	public static partial class ColorExts
	{
		public static ColorDto ToColorDto(this Color source)
		=> new ColorDto
		{
			ColorId = source.ColorId,
			Name = source.Name,
		};
	}
}
