using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.CategoryDtos
{
	public class ChildCategoryDto
	{
		public int ChildCategoryId { get; set; }
		public string Name { get; set; }
		public ParentCategoryDto ParentCategory { get; set; }
	}
	public static partial class ChildCategoryDtoExts
	{
		public static ChildCategoryDto ToChildCategoryDto(this ChildCategory source)
		=> new ChildCategoryDto
		{
			ChildCategoryId = source.ChildCategoryId,
			Name = source.Name,
			ParentCategory = source.ParentCategory.ToParentCategoryDto(),
		};
	}
}
