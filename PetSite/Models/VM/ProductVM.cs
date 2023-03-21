using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Color = PetSite.Models.EFModels.Color;
using Size = PetSite.Models.EFModels.Size;

namespace PetSite.Models.VM
{
    public class ProductVM
    {
		public int Id { get; set; }
		[Display(Name = "商品名稱")]
		[Required(ErrorMessage ="名稱未填")]
		public string Name { get; set; }
		[Display(Name = "價錢")]
		[Required(ErrorMessage = "價錢未填")]

		public decimal Price { get; set; }
		[Display(Name = "商品描述")]
		[Required(ErrorMessage = "描述未填")]

		public string Description { get; set; }
		[Display(Name = "類別")]
		public int CategoryId { get; set; }
		[Display(Name = "是否上架")]
		public bool Status { get; set; }
		[Display(Name = "尺寸")]

		public int SizeId { get; set; }
		[Display(Name = "顏色")]

		public int ColorId { get; set; }
		[Display(Name = "口味")]

		public int FlavorId { get; set; }
		[Display(Name = "新增時間")]

		public DateTime CreateDate { get; set; }
		[Display(Name = "物種")]

		public int SpeciesId { get; set; }
		[Display(Name = "數量")]

		public int Quantity { get; set; }
		public List<IFormFile>? Image { get; set; }
		public string[] ImageName { get; set; }
		public virtual ChildCategory Category { get; set; }
		public virtual Color Color { get; set; }
		public virtual Flavor Flavor { get; set; }
		public virtual Size Size { get; set; }
		public virtual Species Species { get; set; }

	}
	public static class ProductExts
	{
		public static ProductVM ToEntity(this Product source)
			=> new ProductVM
			{
				Id = source.ProductId,
				Name = source.Name,
				Price = source.Price,
				Description = source.Description,
				CreateDate= source.CreateDate,
				Category = source.Category,
				Status = source.Status,
				Size = source.Size,
				Color = source.Color,
				Flavor = source.Flavor,
				Species = source.Species,
				Quantity = source.Quantity,
			};

	}
}

