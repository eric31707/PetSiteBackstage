using PetSite.Models.EFModels;

namespace PetSite.Models.Services
{
    public class ProductService
    {
        private readonly PetSiteContext _context;

        public ProductService(PetSiteContext context)
        {
            _context = context;
        }
    }
}
