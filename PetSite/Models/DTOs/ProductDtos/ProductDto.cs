using PetSite.Models.DTOs.CategoryDtos;
using PetSite.Models.DTOs.PropertiesDtos;
using PetSite.Models.DTOs.PropertyDtos;
using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs.ProductDtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public ChildCategoryDto Category { get; set; }
        public bool Status { get; set; }
        public SizeDto Size { get; set; }
        public ColorDto Color { get; set; }
        public FlavorDto Flavor { get; set; }
        public int Quantity { get; set; }
        public SpeciesDto Species { get; set; }
    }
    public static partial class ProductExts
    {
        public static ProductDto ToProductDto(this Product source)
            => new ProductDto
            {
                ProductId = source.ProductId,
                Name = source.Name,
                Price = source.Price,
                Description = source.Description,
                CreateDate = source.CreateDate,
                Category = source.Category.ToChildCategoryDto(),
                Status = source.Status,
                Size = source.Size.ToSizeDto(),
                Color = source.Color.ToColorDto(),
                Flavor = source.Flavor.ToFlavorDto(),
                Quantity = source.Quantity,
                Species = source.Species.ToSpeciesDto(),

            };
    }
}
