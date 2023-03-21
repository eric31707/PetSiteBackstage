using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.CategoryDtos
{
	public class ParentCategoryDto
	{
		public int ParentCategoryId { get; set; }
		public string Name { get; set; }
		public ProductCategoryDto ProductCategory { get; set; }
	}
	public static partial class ParentCategoryDtoExts
	{
		public static ParentCategoryDto ToParentCategoryDto(this ParentCategory source)
			=> new ParentCategoryDto
			{
				ParentCategoryId = source.ParentCategoryId,
				Name = source.Name,
				ProductCategory=source.ProductCategory.ToProductCategoryDto()
			};
	}
}
