using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Services.Interfaces;
using PetSite.Models.ViewModels;
using System;
using System.Collections.Generic;


namespace PetSite.Infrastructures.Repositories
{
    public class MemberCouponRepository 
    {
        private PetSiteContext db;
        public MemberCouponRepository(PetSiteContext db)
        {
            this.db = db;
        }
        public IEnumerable<MemberCouponDTO> Search()
        {

            var query = db.MemberCoupons
                .Include(x => x.Member)
                .Include(c => c.Coupon)
                .ToList();
            
            return query.Select(x => x.ToMbCouponDTO());

        }
        //public void Save(MemberCouponDTO coupon)
        //{
        //    var cartEF = coupon.ToMbCouponDTO();

        //    var efInDb = db.MemberCoupons
        //        .Include(x => x.CouponId)
        //        .Include(m=> m.MemberId)
        //        .Single(x => x.MemberCouponId == MemberCoupons.Id);

        //    var Couponitems = efInDb.CartItems.ToList();

        //    // 若 efInDb中的商品不存在於 cartEF, 表示使用者刪除了
        //    var notuseCoupon = Couponitems.Select(x => x.CouponId)
        //        .Except(cartEF.CartItems.Select(x => x.ProductId))
        //        .ToList();

        //    foreach (var productId in deletedProducts)
        //    {
        //        var delItem = efInDb.CartItems.Single(x => x.ProductId == productId);
        //        // 不能直接remove item,要標註它已被刪除,才能正常執行
        //        db.Entry(delItem).State = EntityState.Deleted;
        //    }

        //    // 新增商品或異動數量
        //    foreach (var item in cartEF.CartItems)
        //    {
        //        if (item.Id == 0)
        //        {
        //            efInDb.CartItems.Add(item);
        //        }
        //        else
        //        {
        //            efInDb.CartItems.Single(x => x.Id == item.Id).Qty = item.Qty;
        //        }
        //    }

        //    db.SaveChanges();
        //}

    }
}
