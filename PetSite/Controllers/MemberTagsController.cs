using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using PetSite.Models;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Infrastructures.Repositories;
using PetSite.Models.Services;
using PetSite.Models.ViewModels;
using System.Text.Json.Nodes;

namespace PetSite.Controllers
{
	public class MemberTagsController : Controller
	{
		private readonly PetSiteContext _context;
	
		private MemberTagService memberTagService;
		public MemberTagsController(PetSiteContext context)
		{
			var db = new PetSiteContext();
			var memberRepo = new MemberTagRepository(db);
			this.memberTagService = new MemberTagService(memberRepo);
			_context = context;
		}
		private static int selectedId;
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult MemberInTag(int Id )
		{
			var data = _context.MemberTags.Include(x => x.Member)
											.Where(x => x.TagId == Id)
											.Select(x => new MembersInTagDTO { Account = x.Member.Account,Email=x.Member.Email,Name=x.Member.Name });

			//List<MemberTagDTO> data =memberTagService.MembersInTag(id);
			return Json(data);
		}
		
		public async Task<IActionResult> EditMemberInTag(int Id)
		{
			selectedId = Id;
            var IdTag = _context.MemberTags.Where(x => x.TagId == Id).Select(x => new EditMemberInTagDTO { MemberId = x.MemberId, IsInTag = true,MemberTagId=x.MemberTagId }).ToList();
			 
			var Members =  _context.Members.ToList().Select(x => new EditMemberInTagMemberDTO { Name = x.Name, Account = x.Account, Address = x.Address, BirthDate = x.BirthDate, IsConfirmed = x.IsConfirmed, MemberId = x.MemberId,});		 
			var Data = from G in Members join T in IdTag on G.MemberId equals T.MemberId into JoinedMember from T in JoinedMember.DefaultIfEmpty() 
					   select new TagMemberListVM { Account =G.Account, Address = G.Address, BirthDate = G.BirthDate, MemberId = G.MemberId,IsInTag=IdTag.Any(X=>X.MemberId==G.MemberId), IsConfirmed = G.IsConfirmed , Name = G.Name ,MemberTagId=(T==null)? 0:T.MemberTagId};
	
			
            return View(Data);
        }
		[HttpPost]
		public async Task<string> EditCheckedMember([FromBody]List<CheckedMemberDTO> checkedDTO)
		{
			int editedDataCount = 0;
            for (int i = 0; i < checkedDTO.Count(); i++)
			{
				if (checkedDTO[i].IsChecked == true)
				{
					if ((_context.MemberTags.Where(x => x.MemberId == checkedDTO[i].MemberId).Any(x => x.TagId == selectedId)) == false)
					{
						MemberTag MT = new MemberTag
						{
							MemberTagId = checkedDTO[i].MemberTagId,
							MemberId = checkedDTO[i].MemberId,
							TagId = selectedId
						};
						_context.MemberTags.Add(MT);
						editedDataCount++;
					}
				}
				if (checkedDTO[i].IsChecked == false)
				{
                   
                    if (_context.MemberTags.Where(x => x.MemberId == checkedDTO[i].MemberId).Any(x => x.TagId == selectedId))
					{
						//int membertagid = _context.MemberTags.Where(x => x.MemberId == checkedDTO[i].MemberId).Where(x=>x.TagId==selectedId).
						MemberTag MT = new MemberTag
						{
                            MemberTagId = checkedDTO[i].MemberTagId,
                            MemberId = checkedDTO[i].MemberId,
							TagId = selectedId
						};
						_context.MemberTags.Remove(MT);
						editedDataCount++;
					}
				}
			}
			await _context.SaveChangesAsync();
			
			return $"{editedDataCount} 筆資料編輯成功";
		}
	}
}
