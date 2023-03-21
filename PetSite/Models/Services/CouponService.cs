using NuGet.Protocol.Core.Types;
using PetSite.Infrastructures.Repositories;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Services.Interfaces;

namespace PetSite.Models.Services
{
    public class CouponService 
    {
        private readonly CouponRepository repository;
        public CouponService(CouponRepository repo)
        {
            this.repository = repo;
        }
        public void CreateCoupon(CouponDTO dto)
        {
            repository.Create(dto);
        }
        public void ActiveCoupon(int couponId, bool CanUseStatus)
        {
            CouponDTO dto = repository.Load(couponId);
            if (dto == null) return;

            if (dto.Status != false && CanUseStatus != false)  return;

            repository.ActiveCoupon(couponId);
        }
		public void UpdateCouopon(CouponDTO request)
		=> repository.Update(request);
		public CouponDTO Load(int? id)
			=> repository.Load(id);
		public IEnumerable<CouponDTO> Search()
        => repository.Search();
        public IEnumerable<CouponDTO> Search(string discountName)
        => repository.Search(discountName);
    }
}
