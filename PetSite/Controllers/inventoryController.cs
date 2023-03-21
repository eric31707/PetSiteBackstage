using Microsoft.AspNetCore.Mvc;
using PetSite.Models.EFModels;
using Microsoft.EntityFrameworkCore;
namespace PetSite.Controllers
{
	public class inventoryController : Controller
	{
		private readonly PetSiteContext _context;

		public inventoryController(PetSiteContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var data = _context.Products.Include(x=>x.Color).Include(x => x.Size).Include(x => x.Flavor).ToList();
			return View(data);
		}
	}
}
