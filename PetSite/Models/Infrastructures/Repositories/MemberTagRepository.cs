using Microsoft.EntityFrameworkCore;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Services.Interfaces;

namespace PetSite.Models.Infrastructures.Repositories
{
	public class MemberTagRepository : IMemberTagRepository
	{
		private readonly PetSiteContext _context;
		public MemberTagRepository(PetSiteContext context)
		{
			_context= context;
		}
		public IEnumerable<MemberTagDTO> Load(int tagId)
		{
			var entity = _context.MemberTags.Include(x=>x.Member)
											.Where(x =>x.TagId==tagId)
											.Select(x=>new MemberTagDTO {Account=x.Member.Account});
																	
			return entity;
		}
			
	}
}
