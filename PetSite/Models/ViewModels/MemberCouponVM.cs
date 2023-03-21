using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PetSite.Models.ViewModels
{
  
    public class MemberCouponVM
    {
        public int Id { get; set; }
        [Display(Name = "會員帳號")]
        //[Required(ErrorMessage ="請輸入帳號")]
        public string Account { get; set; }
        [Display(Name = "折扣券名稱")]
      
        public string DiscountName { get; set; }
        [Display(Name = "折扣金")]
        public int? Amount { get; set; }
        [Display(Name = "使用情況")]
        public bool UsedCoupon { get; set; }
        [Display(Name = "效期")]
        public bool Status { get; set; }
		[Display(Name = "數量")]
		public int? Qty { get; set; }

	}

    public static partial class MemberCouponDTOExts
    {
        public static MemberCouponVM ToMbCouponVM(this MemberCouponDTO source)
        {
            return new MemberCouponVM
            {
                Id = source.MemberCouponId,
                Account = source.Member.Account,
                DiscountName = source.Coupon.DiscountName,
                Amount=source.Coupon.DiscountAmount,
                UsedCoupon = source.UsedCoupon,
                Status = source.Status

            };
        }
    }
}
