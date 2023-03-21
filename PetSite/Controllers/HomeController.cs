using Microsoft.AspNetCore.Mvc;
using PetSite.Models;
using PetSite.Models.Dtos;
using PetSite.Models.DTOs;

using PetSite.Models.EFModels;
using PetSite.Models.ViewModels;
using System.Data.Entity;
using System.Diagnostics;
using X.PagedList;

namespace PetSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly PetSiteContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, PetSiteContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<JsonResult> GetRevenue()
        {
           var data  = _context.ProductOrders.Where(x => x.CreateDate >= DateTime.Today.AddMonths(-6)).Select(x => new ProductOrderDTO { CreateDate=x.CreateDate,Total=x.Total }).ToList();
           var revenueData = data.GroupBy(x => x.CreateDate.Month).Select(x=> new HomeDTO {Month=x.Key,Revenue=x.Sum(x=>x.Total)}).ToList();
           return Json(revenueData);
        }

		[HttpPost]
		public async Task<JsonResult> GetHotelRevenue()
		{
			var data = _context.RoomOrders.Where(x => x.OrderDate >= DateTime.Today.AddMonths(-6)).Select(x => new ProductOrderDTO { CreateDate = x.OrderDate, Total = x.TotalPrice });
			var revenueData = data.GroupBy(x => x.CreateDate.Month).Select(x => new HomeDTO { Month = x.Key, Revenue = x.Sum(x => x.Total) });
			return Json(revenueData);
		}
		[HttpPost]
		public async Task<JsonResult> GetData()
		{
            var memberAmount = _context.Members.Count();
			var adoptionAmount=_context.Adoptions.Count();
            var eventAmount = _context.ApplyEvents.Count();
            var productAmount = _context.Products.Count();
            var soldProduct = _context.ProductOrderItems.Sum(x=>x.Qty);


            var homeData = new HomeDataDTO { MemberAmount = memberAmount, AdoptionAmount = adoptionAmount, EventAmount = eventAmount 
                                            ,ProductAmount=productAmount,SoldProduct=soldProduct};
			return Json(homeData);
		}
		//[HttpPost]
		//public async Task<JsonResult> GetMemberCount()
		//{
		//	var data = _context.Members.Where(x => x.CreateDate >= DateTime.Today.AddMonths(-6)).Select(x => new MemberCountDTO { CreateDate = x.CreateDate,});
		//	var memberData = data.GroupBy(x => x.CreateDate.Month).Select(x => new HomeDTO { Month = x.Key, Revenue = x.Count() });
		//	return Json(memberData);
		//}
	}
}