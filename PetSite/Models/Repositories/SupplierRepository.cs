using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using System.Xml.Linq;

namespace PetSite.Models.Repositories
{
    public class SupplierRepository
    {
        private readonly PetSiteContext _context;

        public SupplierRepository(PetSiteContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 根據進貨商name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SupplierCreateDTO FindbySupplier(string name)
            => _context.Suppliers.FirstOrDefault(x => x.SupplierName == name).ToDTO();

        public SupplierCreateDTO FindbySupplierByEdit(string name, int id)
            => _context.Suppliers.Where(x => x.SupplierId != id).FirstOrDefault(x => x.SupplierName == name).ToDTO();

        public SupplierCreateDTO FindbySupplierId(int id)
            => _context.Suppliers.FirstOrDefault(x => x.SupplierId == id).ToDTO();
        public void Create(SupplierCreateDTO supplierCreateDTO)
        {
            Supplier supplier = new Supplier
            {
                SupplierName = supplierCreateDTO.SupplierName,
                Mobile = supplierCreateDTO.Mobile,
            };
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }
        public List<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }
        public void Update(SupplierCreateDTO entity)
        {
            var supplier = _context.Suppliers.Find(entity.Id);
            supplier.SupplierName = entity.SupplierName;
            supplier.Mobile = entity.Mobile;
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var supplier = _context.Suppliers.SingleOrDefault(x => x.SupplierId == id);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
    }
}
