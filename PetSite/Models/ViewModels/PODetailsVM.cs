using PetSite.Models.ViewModels.PODetails;

namespace PetSite.Models.ViewModels
{
    public class PODetailsVM
    {

        public int ProductOrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public string OrderStatus { get; set; }
        public string Receiver { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string ShipmentStatus { get; set; }
        public string ShipmentMethod { get; set; }
        public List<ProductOrderDetailVM> OrderDetails { get; set; }
        public decimal Total { get; set; }

    }
}
