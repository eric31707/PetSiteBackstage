using NuGet.Protocol.Core.Types;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Repositories;
using PetSite.Models.ViewModels;

namespace PetSite.Models.Services
{
    public class SupplierService
    {
        private readonly PetSiteContext _context;
        private SupplierRepository repository;
        public SupplierService(PetSiteContext context)
        {
            _context = context;
            repository = new SupplierRepository(_context);
        }
        public void Create(SupplierCreateDTO supplierCreateDTO)
        {
            var dataInDb = repository.FindbySupplier(supplierCreateDTO.SupplierName);
            if (dataInDb != null)
            {
                throw new Exception("請輸入不同的供應商");
            }

            new SupplierRepository(_context).Create(supplierCreateDTO);
        }
        public void UpdateProfile(SupplierCreateDTO supplierCreateDTO)
        {
            SupplierCreateDTO entity = repository.FindbySupplierByEdit(supplierCreateDTO.SupplierName, supplierCreateDTO.Id);
            if (entity != null) throw new Exception("供應商名字重複");
            entity = repository.FindbySupplierId(supplierCreateDTO.Id);
            if (entity == null) throw new Exception("找不到要修改的供應商記錄");

            entity.SupplierName = supplierCreateDTO.SupplierName;
            entity.Mobile = supplierCreateDTO.Mobile;

            repository.Update(entity);
        }
        public void Delete(SupplierCreateDTO supplierCreateDTO)
        {
            SupplierCreateDTO entity = repository.FindbySupplierId(supplierCreateDTO.Id);
            if (entity == null) throw new Exception("找不到要刪除的供應商記錄");

            repository.Delete(entity.Id);
        }
    }
}
