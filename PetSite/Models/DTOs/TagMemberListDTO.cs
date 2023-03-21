namespace PetSite.Models.DTOs
{
    public class TagMemberListDTO
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public bool IsConfirmed { get; set; }

        public int TagId { get; set; }
    }
}
