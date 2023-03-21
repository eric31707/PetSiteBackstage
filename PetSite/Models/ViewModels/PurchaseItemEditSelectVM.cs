using PetSite.Models.EFModels;

namespace PetSite.Models.ViewModels
{
    public class PurchaseItemEditSelectVM
    {
        public string ProductName { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int FlavorId { get; set; }
        public string FlavorName { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}
