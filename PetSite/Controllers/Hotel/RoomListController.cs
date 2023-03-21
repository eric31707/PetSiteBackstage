using Microsoft.AspNetCore.Mvc;

namespace PetSite.Controllers.Hotel
{
    public class RoomListController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
