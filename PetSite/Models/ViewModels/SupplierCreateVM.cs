using PetSite.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PetSite.Models.ViewModels
{
    public class SupplierCreateVM
    {
        public int Id { get; set; }
        [Display(Name = "供應商")]
        [Required]
        public string SupplierName { get; set; }
        [Display(Name = "電話")]
        [Required]
        public string Mobile { get; set; }
    }
    public static class SupplierCreateVMEXts
    {
        public static SupplierCreateVM ToCreateVM(this SupplierCreateDTO source)
        {
            return new SupplierCreateVM
            {
                Id = source.Id,
                SupplierName = source.SupplierName,
                Mobile = source.Mobile,
            };
        }
    }
}
