using PetSite.Models.EFModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace PetSite.Models.DTOs
{
    public class MemberCouponDTO
    {
        public int MemberCouponId { get; set; }
        public MemberDTO Member { get; set; }
        public CouponDTO Coupon{ get; set; }
        public CouponDTO Amount { get; set; }
        public bool UsedCoupon { get; set; }
        public bool Status { get; set; }
		public int? Qty { get; set; }


	}
    public static partial class MemberCouponExts
    {
        public static MemberCouponDTO ToMbCouponDTO(this MemberCoupon source)
            => new MemberCouponDTO
            {
                MemberCouponId = source.MemberCouponId,
                Member = source.Member.ToEntity(),
                Coupon = source.Coupon.ToEntity(),
                Amount= source.Coupon.ToEntity(),
                UsedCoupon = source.UsedCoupon,
                Status = source.Status,
                Qty = source.Qty,
            };
    }
  
   
    //public void AddItem(CouponDTO coupon, MemberDTO member )
    //{
    //    if (member.IsConfirmed == false) return;
    //    else
    //    {
             
    //    };
        
    //}
    //public IEnumerable<MemberVM> GetMembers()
    //{
       
    //}
}
