using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Services.Interfaces;
using PetSite.Models.ViewModels;
using System;

namespace PetSite.Infrastructures.Repositories
{
    public class CouponRepository : ICouponRepository
    {

        private readonly PetSiteContext _db;

        public CouponRepository(PetSiteContext db)
        {
            _db = db;
        }
		public void Create(CouponDTO dto)
		{
			Coupon coupon = new Coupon
			{
				DiscountCode = dto.DiscountCode,
				Conditions = dto.Conditions,
				DiscountAmount = dto.DiscountAmount,
				DiscountName = dto.DiscountName,
				UserType = dto.UserType,
				StartDate = dto.StartDate, //預設是未確認的會員
				EndDate = dto.EndDate,
                Qty = dto.Qty,
                Status = dto.Status,
			};

			_db.Coupons.Add(coupon);
			_db.SaveChanges();
		}
		public IEnumerable<CouponDTO> Search()
        {
            IEnumerable<Coupon> query = _db.Coupons;
            //if (couponId.HasValue) query = query.Where(x => x.CouponId == couponId);
            //if (!string.IsNullOrEmpty(discountName)) query = query.Where(x => x.DiscountName.Contains(discountName));
            //if (status.HasValue) query = query.Where(x => x.Status == status);
            //query = query.OrderBy(x => x.DiscountName);

            return query.Select(x => x.ToEntity());
        }
		public IEnumerable<CouponDTO> Search(string discountName)
		{
			IEnumerable<Coupon> query = _db.Coupons;
			
			if (!string.IsNullOrEmpty(discountName)) query = query.Where(x => x.DiscountName.Contains(discountName));
			
			query = query.OrderBy(x => x.DiscountName);

			return query.Select(x => x.ToEntity());
		}

		public CouponDTO Load(int? couponId)
        {
            Coupon entity = _db.Coupons.SingleOrDefault(x => x.CouponId == couponId);
            

            CouponDTO result = new CouponDTO
            {
               CouponId = entity.CouponId,
                DiscountCode = entity.DiscountCode,
                DiscountName = entity.DiscountName,
                
                Conditions = entity.Conditions,
                DiscountAmount = entity.DiscountAmount,
                UserType = entity.UserType,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Qty = entity.Qty,
                Status = entity.Status
            };

            return entity.ToEntity();
        }
		public void Update(CouponDTO model)
		{

			var coupon = _db.Coupons.SingleOrDefault(x => x.CouponId == model.CouponId);

			coupon.CouponId = model.CouponId;
			coupon.Status = model.Status;
			coupon.DiscountCode = model.DiscountCode;
			coupon.DiscountName = model.DiscountName;
			coupon.DiscountAmount = model.DiscountAmount;
			coupon.Conditions = model.Conditions;
			coupon.UserType = model.UserType;
			coupon.EndDate = model.EndDate;
			coupon.StartDate = model.StartDate;
			coupon.Qty = model.Qty;

			_db.SaveChanges();

		}
		public void ActiveCoupon(int couponId)
        {
            var coupon = _db.Coupons.Find(couponId);
            coupon.Status = true;
           
            _db.SaveChanges();
        }

        //找到可用的CouponId 以及是否狀態status上架可使用
        public CouponDTO Load(int couponId, bool? status)
        {
            IEnumerable<Coupon> query = _db.Coupons.Where(x => x.CouponId == couponId);
            if (status.HasValue) query = query.Where(x => x.Status == status);

            var coupon = query.FirstOrDefault();

            return coupon == null ? null : coupon.ToEntity();
        }
        //public ProductDto Load(int productId, bool? status)
        //{
        //    IEnumerable<Product> query = _db.Products.Where(x => x.Id == productId);
        //    if (status.HasValue) query = query.Where(x => x.Status == status);

        //    var product = query.FirstOrDefault();

        //    return product == null ? null : product.ToEntity();
        //}
    }
}
