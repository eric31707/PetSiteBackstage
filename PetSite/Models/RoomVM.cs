using Microsoft.AspNetCore.Mvc;
using PetSite.Models.EFModels;
using System.ComponentModel.DataAnnotations;

namespace PetSite.Models
{
    public class RoomVM
    {
        public int RoomId { get; set; }
        [Required]
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


    public static class RoomExts
    {
        public static RoomVM ToEntity(this Room source)
            => new RoomVM
            {
                RoomId = source.RoomId,
                Type = source.Type,
                Name = source.Name,
                Price = source.Price,
                Address = source.Address,
            };
        public static UpdateRoomVM ToVM(this RoomVM source)
        {
            return new UpdateRoomVM
            {
                Type = source.Type,
                Name = source.Name,
                Price = source.Price,
                Address = source.Address,
            };
        }
    }
}
