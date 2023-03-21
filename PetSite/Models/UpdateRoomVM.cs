using System.ComponentModel.DataAnnotations;

namespace PetSite.Models
{
    public class UpdateRoomVM
    {
        public int RoomId { get; set; }

        public string Type { get; set; }
        [Required(ErrorMessage = "名稱必填")]
        public string Name { get; set; }
        [Required(ErrorMessage = "價格必填")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "地址必填")]
        public string Address { get; set; }
        public List<IFormFile>? Photo { get; set; }

        public string[] fileName { get; set; }

    }
}
