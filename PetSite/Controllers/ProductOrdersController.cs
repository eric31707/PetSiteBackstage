using Microsoft.AspNetCore.Mvc;
using PetSite.Models.EFModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using PetSite.Models.ViewModels;
using PetSite.Models.ViewModels.PODetails;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetSite.Models.Infrastructures.ExtensionMethods;

namespace PetSite.Controllers
{
    public class ProductOrdersController : Controller
    {
        private readonly PetSiteContext _db;
        public ProductOrdersController(PetSiteContext db)
        {
            _db = db;
        }

        public IActionResult Index(string? orderStatus, string? paymentStatus, string? shipmentStatus,string? receiver,int pageNumber = 1)
        {
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            var productOrders = GetPoIndexVM(orderStatus,paymentStatus,shipmentStatus,pageNumber);
            return View(productOrders);
        }


        private IPagedList<POIndexVM> GetPoIndexVM(string? orderStatus, string? paymentStatus, string? shipmentStatus,int pageNumber)
        {
            int pagesize = 10;
            
            IQueryable<ProductOrder> query = _db.ProductOrders;
            if (!string.IsNullOrEmpty(orderStatus))
            {
                query = query.Where(x => x.OrderStatus == orderStatus);
            }
            if (!string.IsNullOrEmpty(paymentStatus)) 
            { 
                 query = query.Where(x => x.PaymentStatus == paymentStatus); 
            }
            if (!string.IsNullOrEmpty(shipmentStatus))                                                                                                                                                                                                                      
            {
                 query = query.Where(x => x.ShipmentStatus == shipmentStatus);
            }
            
            var ProductOrders=query.OrderBy(x => x.ProductOrderId).Select(p => p.ToPoIndexVM()).ToPagedList(pageNumber, pagesize);

            return ProductOrders;

            //var productOrders = _db.ProductOrders
            //    .OrderBy(x=>x.ProductOrderId)
            //    .Select(p => p.ToPoIndexVM())
            //    .ToPagedList(pageNumber, pagesize);
            //return productOrders;
        }
        //public IActionResult OrderStatus(int id)
        //{
        //    var orderStatus = _db.ProductOrders
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToOrderStatusVM())
        //        .Single();

        //    return View(orderStatus);
        //}

        //public IActionResult ReceiverDetail(int id)
        //{
        //    var receiverDetail = _db.ProductOrders
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToReceiverDetailVM())
        //        .Single();
        //    return View(receiverDetail);
        //}
        //public IActionResult PaymentStatus(int id)
        //{
        //    var paymentStatus = _db.ProductOrders
        //        .Include(x => x.Payment)
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToPaymentStatusVM())
        //        .Single();

        //    return View(paymentStatus);
        //}
        //public IActionResult ShipmentStatus(int id)
        //{
        //    var shipmentStatus = _db.ProductOrders
        //        .Include(x => x.Shipment)
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToShipmentStatusVM())
        //        .Single();
        //    return View(shipmentStatus);
        //}
        //public IActionResult ProductDetail(int id)

        //{
        //    var productDetail = _db.ProductOrderItems
        //        .Include(x => x.ProductOrder)
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToProductOrderDetailVM())
        //        .ToList();
        //    var productOrderInfo = new ProductInfoVM
        //    {
        //        OrderDetails = productDetail,
        //        Total = productDetail.Sum(x => x.SubTotal),
        //    };

        //    return View(productOrderInfo);
        //}
        //public IActionResult PODetails(int id)
        //{

        //    var orderStatus = _db.ProductOrders
        //       .Where(x => x.ProductOrderId == id)
        //       .Select(p => p.ToOrderStatusVM())
        //       .Single();
        //    var receiverDetail = _db.ProductOrders
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToReceiverDetailVM())
        //        .Single();
        //    var paymentStatus = _db.ProductOrders
        //        .Include(x => x.Payment)
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToPaymentStatusVM())
        //        .Single();
        //    var shipmentStatus = _db.ProductOrders
        //        .Include(x => x.Shipment)
        //        .Where(x => x.ProductOrderId == id)
        //        .Select(p => p.ToShipmentStatusVM())
        //        .Single();
        //    var productDetail = _db.ProductOrderItems
        //       .Include(x => x.ProductOrder)
        //       .Where(x => x.ProductOrderId == id)
        //       .Select(p => p.ToProductOrderDetailVM())
        //       .ToList();
        //    var productOrderInfo = new ProductInfoVM
        //    {
        //        OrderDetails = productDetail,
        //        Total = productDetail.Sum(x => x.SubTotal),
        //    };
        //    var groupPoDetails = new PODetailsVM
        //    {
        //        OrderStatusViewModel = orderStatus,
        //        ReceiverDetail = receiverDetail,
        //        PaymentStatus = paymentStatus,
        //        ShipmentStatus = shipmentStatus,
        //        ProductInfo = productOrderInfo,
        //    };
        //    ViewBag.orderStatus = GetOrderStatusSelectList().ToList();
        //    ViewBag.paymentStatus = GetPaymenyStatusSelectList().ToList();
        //    ViewBag.shipmentStatus = GetShipmentStatusSelectList().ToList();

        //    return View(groupPoDetails);
        //}

        public IActionResult PODetails(int id)
        {

            var data = _db.ProductOrders
                .Include(x => x.Payment)
                .Include(x => x.Shipment)
                .Where(x => x.ProductOrderId == id)
                .Select(p => p.ToPODetailVM())
                .Single();
            var productDetail = _db.ProductOrderItems
               .Include(x => x.ProductOrder)
               .Where(x => x.ProductOrderId == id)
               .Select(p => p.ToProductOrderDetailVM())
               .ToList();
            var data1 = new PODetailsVM
            {
                ProductOrderId = data.ProductOrderId,
                CreateDate = data.CreateDate,
                OrderStatus = data.OrderStatus,
                Receiver = data.Receiver,
                Address = data.Address,
                Mobile = data.Mobile,
                PaymentStatus = data.PaymentStatus,
                PaymentMethod = data.PaymentMethod,
                ShipmentStatus = data.ShipmentStatus,
                ShipmentMethod = data.ShipmentMethod,
                OrderDetails = productDetail,
                Total = productDetail.Sum(x => x.SubTotal),
            };
            ViewBag.orderStatus = GetOrderStatusSelectList().ToList();
            ViewBag.paymentStatus = GetPaymentStatusSelectList().ToList();
            ViewBag.shipmentStatus = GetShipmentStatusSelectList().ToList();
            return View(data1);
        }
        [HttpPost]
        public IActionResult PODetails(PODetailsVM model)
        {
            var data = _db.ProductOrders
                .Include(x => x.Payment)
                .Include(x => x.Shipment)
                .Where(x => x.ProductOrderId == model.ProductOrderId)
                .Single();
            data.OrderStatus = model.OrderStatus;
            data.PaymentStatus = model.PaymentStatus;
            data.ShipmentStatus = model.ShipmentStatus;
            _db.SaveChanges();
            return RedirectToAction("PODetails", new { id = model.ProductOrderId });
        }
        private IEnumerable<SelectListItem> GetOrderStatusSelectList()
        {
            var orderStatusSelectList = new List<SelectListItem>()
            {
               new SelectListItem()
               {
                   Text="處理中",
                   Value="處理中",
               },
               new SelectListItem()
               {

                     Text="已確認",
                     Value="已確認",

               },
               new SelectListItem()
               {
                   Text="已完成",
                   Value="已完成",


               },
               new SelectListItem()
               {
                   Text="已取消",
                   Value="已取消"
               }

            };
            return orderStatusSelectList;
        }
        private IEnumerable<SelectListItem> GetPaymentStatusSelectList()
        {

            var paymentStatusSelectList = new List<SelectListItem>()
            {
               new SelectListItem()
               {
                   Text="未付款",
                   Value="未付款",
               },
               new SelectListItem()
               {

                     Text="付款失敗",
                     Value="付款失敗",

               },
               new SelectListItem()
               {
                   Text="已付款",
                   Value="已付款"
               },
               new SelectListItem()
               {
                   Text="已退款",
                   Value="已退款"
               }

            };
            return paymentStatusSelectList;
        }
        private IEnumerable<SelectListItem> GetShipmentStatusSelectList()
        {
            var shipmentStatusSelectList = new List<SelectListItem>()
            {
               new SelectListItem()
               {
                   Text="備貨中",
                   Value="備貨中",
               },
               new SelectListItem()
               {

                     Text="發貨中",
                     Value="發貨中",

               },
               new SelectListItem()
               {
                   Text="已出貨",
                   Value="已出貨",


               },
               new SelectListItem()
               {
                   Text="已送達",
                   Value="已送達"
               },
               new SelectListItem()
               {
                   Text="退款中",
                   Value="退款中"
               },
               new SelectListItem()
               {
                   Text="已取消",
                   Value="已取消"
               },
               new SelectListItem()
               {
                   Text="退貨中",
                   Value="退貨中"
               },
               new SelectListItem()
               {
                   Text="已退貨",
                   Value="已退貨"
               }

            };
            return shipmentStatusSelectList;
        }
    }
}
