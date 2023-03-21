
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace PetSite.Models.ViewModels
{
	public class CouponVM
	{
		public int CouponId { get; set; }
		[Display(Name = "折扣碼")]
		[Required(ErrorMessage = "{0}必填")]
		public string DiscountCode { get; set; }
		
		[Display(Name = "折扣券名稱")]
		[Required(ErrorMessage = "{0}必填")]
		public string DiscountName { get; set; }
		[Display(Name = "滿額")]
		
		public int? Conditions { get; set; }
		
		[Display(Name = "折扣金額")]
		[Required(ErrorMessage ="折扣金額必填")]
		public int? DiscountAmount { get; set; }
		[Display(Name = "使用者")]
		[Required(ErrorMessage = "{0}必填")]
		public string UserType { get; set; }
		[Display(Name = "啟用時間")]
		public DateTime? StartDate { get; set; }
		[Display(Name = "截止時間")]
		public DateTime? EndDate { get; set; }
		[Display(Name = "數量")]
		public int? Qty { get; set; }
		[Display(Name = "啟用")]
		[Required(ErrorMessage = "{0}必選")]
		public bool Status { get; set; }
	}

	public static partial class CouponDTOExts
	{
		public static CouponVM ToCouponVM(this CouponDTO source)
		{
			return new CouponVM
			{
				CouponId = source.CouponId,
				DiscountCode = source.DiscountCode,
				
				DiscountName = source.DiscountName,
                Conditions = source.Conditions,
                DiscountAmount = source.DiscountAmount,
				UserType = source.UserType,
				StartDate= source.StartDate,
				EndDate= source.EndDate,
				Qty = source.Qty,
				Status = source.Status

			};
		}
	}
}
