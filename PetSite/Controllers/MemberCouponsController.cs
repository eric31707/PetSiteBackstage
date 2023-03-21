using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSite.Infrastructures.Repositories;
using PetSite.Models.EFModels;
using PetSite.Models.Services;
using PetSite.Models.Services.Interfaces;
using PetSite.Models.ViewModels;
using System;
using System.Security.Permissions;

namespace PetSite.Controllers
{
    public class MemberCouponsController : Controller
    {
        private MemberCouponService memberCouponService;
        private PetSiteContext _db;
        public MemberCouponsController(PetSiteContext db)
        {
            _db = db;
            MemberCouponRepository repo = new MemberCouponRepository(db);
            this.memberCouponService = new MemberCouponService(repo);
        }

        public IActionResult Index()
        {
            var data = memberCouponService.Search();
            var data1 = data.Select(x => x.ToMbCouponVM()).ToList();
            return View(data1);
        }
        
        //Todo 會員註冊時，自動生成註冊優惠券
        public IActionResult RegisterMbCoupon(int memberId, bool isconfirm, int couponId)
        {
            return View();
        }
        //Todo 會員生日時，發送生日優惠券
        public IActionResult BdMbCoupon(int memberId, DateTime birth, int couponId)
        {
            return View();
        }
        //Todo前台兌換折價券時，建立會員個人使用折價券
        public IActionResult ExchangeMbCoupon(int memberId, int couponId)
        {

            return View();
        }
        
        //public string MemberAccount => User.Identity.Name;
        //[Authorize]//有權限者才可檢視
        //public ActionResult Info()
        //{
        //    var cart = MemberService.Current(MemberAccount);
        //    return View(cart);
        //}
    }
}
