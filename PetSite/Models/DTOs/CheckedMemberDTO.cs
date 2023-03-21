namespace PetSite.Models.DTOs
{
	public class CheckedMemberDTO
	{
		public int MemberId { get; set; }

		public bool IsChecked { get; set; }

		public int MemberTagId { get; set; }
	}
}
