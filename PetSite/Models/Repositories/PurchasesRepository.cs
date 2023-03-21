using Microsoft.EntityFrameworkCore;
using PetSite.Models.EFModels;
using PetSite.Models.ViewModels;


namespace PetSite.Models.Repositories
{
    public class PurchasesRepository
    {
        private readonly PetSiteContext _context;

        public PurchasesRepository(PetSiteContext context)
        {
            _context = context;
        }
        public List<Purchase> GetAll()
        {
            return _context.Purchases.Include(x => x.Supplier).ToList();
        }

    }
}
