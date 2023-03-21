using NuGet.Protocol.Core.Types;
using PetSite.Infrastructures.Repositories;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Services.Interfaces;

namespace PetSite.Models.Services
{
    public class MemberCouponService
    {
        private readonly MemberCouponRepository _MbCouponRepo;
        private readonly CouponRepository _CouponRepo;
        private readonly MemberRepository _MemberRepo;

        public MemberCouponService(CouponRepository Couponrepo,
                            MemberRepository Memberrepo,
                            MemberCouponRepository MbCouponrepo)
        {
            _MemberRepo = Memberrepo;
            _CouponRepo = Couponrepo;
            _MbCouponRepo = MbCouponrepo;
        }
        public MemberCouponService(MemberCouponRepository repository)
        {
            _MbCouponRepo = repository;
        }
        public IEnumerable<MemberCouponDTO> Search()

            => _MbCouponRepo.Search();
        //public MemberCouponDTO Current(string customerAccount)
        //{
            
        //}
        //public void AddItem(string memberAccount, int couponId, int qty = 1)
        //{
        //    var cart = Current(memberAccount);

        //    var coupon = _CouponRepo.Load(couponId, true);
        //    var coupondto = new CouponDTO();
        //    var cartProd = new CouponDTO { CouponId = couponId, DiscountName = coupondto.DiscountName, DiscountAmount = coupondto.DiscountAmount };
        //    cart.AddItem(cartProd, qty);

        //    _MbCouponRepo.Save(cart);
        //}
    }
}
