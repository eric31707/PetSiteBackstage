using PetSite.Models.DTOs;

namespace PetSite.Models.Services.Interfaces
{
    public interface ICouponRepository
    {
        CouponDTO Load(int couponId, bool? status);
        void Create(CouponDTO dto);
        public IEnumerable<CouponDTO> Search(string discountName);
		IEnumerable<CouponDTO> Search();
        void ActiveCoupon(int couponId);
		CouponDTO Load(int? couponId);
		void Update(CouponDTO model);
	}
}