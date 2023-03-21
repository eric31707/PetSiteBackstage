using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSite.Infrastructures.Repositories;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Services;
using PetSite.Models.ViewModels;
using X.PagedList;

namespace PetSite.Controllers
{
    public class CouponsController : Controller
    {
        private readonly PetSiteContext _context;
        private CouponService cpservice;

        public CouponsController(PetSiteContext context)
        {
            _context = context;
            CouponRepository repo = new CouponRepository(context);
            this.cpservice = new CouponService(repo);
        }

        // GET: Coupons
        //public IActionResult Index()
        //{


        //    var data = cpservice.Search();
        //    var data1 = data.Select(x => x.ToCouponVM()).ToList();
        //    return View(data1);

        //}

        public IActionResult Index(string discountName, int pageNumber = 1)
        {

            

			var data = cpservice.Search(discountName);
           
            if (string.IsNullOrEmpty(discountName) == false) data = data.Where(p => p.DiscountName.Contains(discountName));
			IPagedList<CouponDTO> pagedData = GetPagedCoupons(discountName, pageNumber);

			ViewBag.DiscountName = discountName;
			ViewBag.PagedData = pagedData;

			return View(pagedData);

        }
		private IPagedList<CouponDTO> GetPagedCoupons( string discountName, int pageNumber)
		{
			int pageSize = 10;
            var dto = new CouponDTO();
			var query = cpservice.Search(discountName);

			// 若有篩選 discountName
			if (string.IsNullOrEmpty(discountName) == false) query = query.Where(p => p.DiscountName.Contains(discountName));

			query = query.OrderBy(x => x.DiscountName);

			return query.ToPagedList(pageNumber, pageSize);
		}

		// GET: Coupons/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Coupons == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(m => m.CouponId == id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        // GET: Coupons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CouponId,DiscountCode,DiscountName,DiscountAmount,Conditions,ConditionAmount,UserType,StartDate,EndDate,Qty,Status")] CouponVM coupon)
        {
            
            if (ModelState.IsValid)
            {
                cpservice.CreateCoupon(coupon.ToEntity());
				return RedirectToAction(nameof(Index));
				
			}
			return View(coupon);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			//if (id == null || _context.Coupons == null)
			//{
			//	return NotFound();
			//}

			//var coupon = await _context.Coupons.FindAsync(id);
			//if (coupon == null)
			//{
			//	return NotFound();
			//}
			//return View(coupon);
			var data = cpservice.Load(id);
			var data1 = data.ToCouponVM();
			return View(data1);
		}

		// POST: Coupons/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("CouponId,DiscountCode,DiscountName,DiscountAmount,Conditions,ConditionAmount,UserType,StartDate,EndDate,Qty,Status")] CouponVM coupon)
		{
			//if (id != coupon.CouponId)
			//{
			//	return NotFound();
			//}

			//if (ModelState.IsValid)
			//{
			//	try
			//	{
			//		_context.Update(coupon);
			//		await _context.SaveChangesAsync();
			//	}
			//	catch (DbUpdateConcurrencyException)
			//	{
			//		if (!CouponExists(coupon.CouponId))
			//		{
			//			return NotFound();
			//		}
			//		else
			//		{
			//			throw;
			//		}
			//	}
			//	return RedirectToAction(nameof(Index));
			//}
			//return View(coupon);
			var data = coupon.ToEntity();
			cpservice.UpdateCouopon(data);
			return RedirectToAction(nameof(Index));
		}

		// GET: Coupons/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coupons == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(m => m.CouponId == id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coupons == null)
            {
                return Problem("Entity set 'PetSiteContext.Coupons'  is null.");
            }
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon != null)
            {
                _context.Coupons.Remove(coupon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponExists(int id)
        {
            return _context.Coupons.Any(e => e.CouponId == id);
        }
    }
}
