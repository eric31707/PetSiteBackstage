namespace PetSite.Models.DTOs
{
	public class MemberTagDTO
	{
		public int MemberTagId { get; set; }
		public int MemberId { get; set; }
		public int TagId { get; set; }

		public string Name { get; set; }

		public string Account { get; set; }

		public string Email { get; set; }
	}
}
