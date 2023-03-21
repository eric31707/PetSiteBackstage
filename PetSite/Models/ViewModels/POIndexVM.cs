using PetSite.Models.EFModels;

namespace PetSite.Models.ViewModels
{
    public class POIndexVM
    {
        public int Id { get; set; }
        public int ProductOrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus  { get; set; }
        public string ShipmentStatus { get; set; }
        public string Receiver { get; set; }
        public decimal Total { get; set; }

    }
    
}
