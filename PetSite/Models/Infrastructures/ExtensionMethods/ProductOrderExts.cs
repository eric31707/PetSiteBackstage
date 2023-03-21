using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using PetSite.Models.EFModels;
using PetSite.Models.ViewModels;
using PetSite.Models.ViewModels.PODetails;

namespace PetSite.Models.Infrastructures.ExtensionMethods
{
    public static class ProductOrderExts
    {

        public static POIndexVM ToPoIndexVM(this ProductOrder source) => new POIndexVM
        {
            ProductOrderId = source.ProductOrderId,
            CreateDate = source.CreateDate,
            OrderStatus = source.OrderStatus,
            PaymentStatus = source.PaymentStatus,
            ShipmentStatus = source.ShipmentStatus,
            Receiver = source.Receiver,
            Total = source.Total,
        };
        public static PODetailsVM ToPODetailVM(this ProductOrder source) => new PODetailsVM
        {
            ProductOrderId = source.ProductOrderId,
            CreateDate = source.CreateDate,
            OrderStatus = source.OrderStatus,
            Receiver = source.Receiver,
            Address = source.Address,
            Mobile = source.Mobile,
            PaymentStatus = source.PaymentStatus,
            PaymentMethod = source.Payment.PaymentMethod,
            ShipmentStatus = source.ShipmentStatus,
            ShipmentMethod = source.Shipment.ShipmentMethod,
            OrderDetails = source.ProductOrderItems.Select(x => x.ToProductOrderDetailVM()).ToList(),
            Total = source.Total,
        };

        //將訂單的item存在ProductOrderDetailVM
        public static ProductOrderDetailVM ToProductOrderDetailVM(this ProductOrderItem source) => new ProductOrderDetailVM
        {
            ProductName = source.ProductName,
            Price = source.Price,
            Qty = source.Qty,
            SubTotal = source.SubTotal,
        };

    }
}
