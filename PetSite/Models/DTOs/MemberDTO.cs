using PetSite.Models.EFModels;

namespace PetSite.Models.DTOs
{
	public class MemberDTO
	{
		public int MemberId { get; set; }
		public string Name { get; set; }
		public string Account { get; set; }
		public string EncryptedPassword { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public string BirthDate { get; set; }
		public string Address { get; set; }
		public bool IsConfirmed { get; set; }
		public string ConfirmCode { get; set; }
      
	}
    public static class MemberExts
    {
        public static MemberDTO ToEntity(this Member source)
            => new MemberDTO
            {
                MemberId = source.MemberId,
                Name = source.Name,
                Account = source.Account,
                EncryptedPassword = source.EncryptedPassword,
                Mobile = source.Mobile,
                Email = source.Email,
                BirthDate = source.BirthDate,
                Address = source.Address,
                IsConfirmed = source.IsConfirmed,
                
            };
    }
}
