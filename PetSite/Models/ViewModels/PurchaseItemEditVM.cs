using System.ComponentModel.DataAnnotations;

namespace PetSite.Models.ViewModels
{
    public class PurchaseItemEditVM
    {
        public List<int> Id { get; set; }
        public string SupplierName { get; set; }
        public int PurchaseId { get; set; }
        public List<string> ProductName { get; set; }
        public List<string> Size { get; set; }
        public List<string> Color { get; set; }
        public List<string> Flavor { get; set; }
        [Required]
        public decimal[] Price { get; set; }
        [Required]
        public int[] Qty { get; set; }
    }


}
