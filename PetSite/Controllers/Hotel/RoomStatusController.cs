using Microsoft.AspNetCore.Mvc;
using PetSite.Models.EFModels;
using PetSite.Models.ViewModels;
using System.Collections.Generic;

namespace PetSite.Controllers.Hotel
{
    public class RoomStatusController : Controller
    {
        private readonly PetSiteContext _context;       

        public RoomStatusController(PetSiteContext context)
        {
            _context = context;           
        }


        public IActionResult Status()
        {
            var orderRooms = _context.RoomOrderItems.ToList();
            
                return View(orderRooms);
        }
        public IActionResult getStatus()
        {
            var orderRooms = _context.RoomOrderItems.ToList();


            return Json(orderRooms);
        }

     
        public async Task<IActionResult> _PartialStatus(int orderId,int roomId)
        {
            if (orderId == null || _context.RoomOrderItems == null|| roomId==null)
            {
                return NotFound();
            }           

           var query= from m in _context.Members
                      join ro in _context.RoomOrders
                      on m.MemberId equals ro.MemberId
                      join roi in _context.RoomOrderItems
                      on ro.RoomOrderId equals roi.RoomOrderId
                      where ro.RoomOrderId==orderId&&roi.RoomId== roomId
                      select new {
                          ro.RoomOrderId,
                          roi.RoomId,
                          roi.StartDate,
                          roi.EndDate,
                          m.Name,
                          ro.TotalPrice,
                          ro.OrderDate
                        };

            var roomorder = query.FirstOrDefault();

            var roomorderdetail = new RoomOrderDetailVM()
            {
                RoomOrderId=roomorder.RoomOrderId,
                RoomId=roomorder.RoomId,
                StartDate=roomorder.StartDate,
                EndDate=roomorder.EndDate,
                MemberName=roomorder.Name,
                TotalPrice=roomorder.TotalPrice,
                OrderDate=roomorder.OrderDate,
            };

            return View(roomorderdetail);
        }

	}
}
