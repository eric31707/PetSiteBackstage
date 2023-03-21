namespace PetSite.Models.ViewModels
{
    public class RoomOrderDetailVM
    {
       public int RoomOrderId { get; set; }
        public int RoomId { get; set; }       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public string  MemberName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
