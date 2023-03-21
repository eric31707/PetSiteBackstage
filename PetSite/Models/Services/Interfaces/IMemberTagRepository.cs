using PetSite.Models.DTOs;

namespace PetSite.Models.Services.Interfaces
{
    public interface IMemberTagRepository
    {
		IEnumerable<MemberTagDTO> Load(int tagId);

	}
}
