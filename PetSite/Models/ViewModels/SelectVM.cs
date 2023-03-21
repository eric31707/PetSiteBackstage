using PetSite.Models.EFModels;

namespace PetSite.Models.ViewModels
{
	public class SelectVM
	{
		public List<Product> ProductItem { get; set; }
        public List<Size> SizeItem { get; set; }
        public List<Color> ColorItem { get; set; }
        public List<Flavor> FlavorItem { get; set; }

    }

}
