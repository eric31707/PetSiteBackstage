using PetSite.Models.DTOs;
using PetSite.Models.Infrastructures.Repositories;
using PetSite.Models.Services.Interfaces;

namespace PetSite.Models.Services
{
	
	public class MemberTagService
	{
		private readonly IMemberTagRepository _repository;
		public MemberTagService(IMemberTagRepository repository)
		{
			_repository = repository;
		}

		public  List <MemberTagDTO> MembersInTag(int id)
		{
		   List<MemberTagDTO> result = _repository.Load(id).ToList();
			return result;
		}
	}
}
