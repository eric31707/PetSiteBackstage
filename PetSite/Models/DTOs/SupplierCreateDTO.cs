using PetSite.Models.EFModels;
using PetSite.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PetSite.Models.DTOs
{
    public class SupplierCreateDTO
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Mobile { get; set; }
    }
    public static class SupplierCreateVMEXts
    {
        public static SupplierCreateDTO ToDTO(this SupplierCreateVM source)
        {
            return new SupplierCreateDTO
            {
                Id = source.Id,
                SupplierName = source.SupplierName,
                Mobile = source.Mobile,
            };
        }
    }
    public static class SupplierEXts
    {
        public static SupplierCreateDTO ToDTO(this Supplier source)
        {
            return source == null ? null : new SupplierCreateDTO
            {
                Id = source.SupplierId,
                SupplierName = source.SupplierName,
                Mobile = source.Mobile,
            };
        }
    }
}
