using PetSite.Models.EFModels;

namespace PetSite.Models.ViewModels
{
    public class RoomOrderItemVM
    {
        public int RoomOrderItemId { get; set; }
        public int RoomOrderId { get; set; }
        public int RoomId { get; set; }
        public decimal RoomPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
      
    }
}
