namespace PetSite.Models.ViewModels.PODetails
{
    public class ProductInfoVM
    {
        public List<ProductOrderDetailVM> OrderDetails { get; set; }
        public decimal Total { get; set; }
    }
}
