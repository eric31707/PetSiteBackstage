using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.CategoryDtos
{
    public class ProductCategoryDto
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
    }
    public static class ProductCategoryExts
    {
        public static ProductCategoryDto ToProductCategoryDto(this ProductCategory source)
            => new ProductCategoryDto
            {
                ProductCategoryId = source.ProductCategoryId,
                Name = source.Name,
            };
    }
}
