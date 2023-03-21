namespace PetSite.Models.ViewModels.PODetails
{
    //存放訂單的productOrderItem
    public class ProductOrderDetailVM
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal SubTotal { get; set; }
    }
}
