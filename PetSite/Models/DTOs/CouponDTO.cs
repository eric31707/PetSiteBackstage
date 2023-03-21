using PetSite.Models.EFModels;
using PetSite.Models.ViewModels;

namespace PetSite.Models.DTOs
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string DiscountCode { get; set; }
        public string DiscountName { get; set; }
       
        public int? Conditions { get; set; }
        public int? DiscountAmount { get; set; }
        public string UserType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Qty { get; set; }
        public bool Status { get; set; }
    }

    
    public static class CouponExts
    {
        public static CouponDTO ToEntity(this Coupon source)
            => new CouponDTO
            {
                CouponId = source.CouponId,
                DiscountCode = source.DiscountCode,
                DiscountName = source.DiscountName,
                Conditions = source.Conditions,
                DiscountAmount = source.DiscountAmount,
                UserType = source.UserType,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Qty = source.Qty,
                Status = source.Status,
            };
		public static CouponDTO ToEntity(this CouponVM source)
			=> new CouponDTO
			{
				CouponId = source.CouponId,
				DiscountCode = source.DiscountCode,
				DiscountName = source.DiscountName,
				Conditions = source.Conditions,
				DiscountAmount = source.DiscountAmount,
				UserType = source.UserType,
				StartDate = source.StartDate,
				EndDate = source.EndDate,
				Qty = source.Qty,
				Status = source.Status,
			};
	}

}
