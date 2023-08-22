using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces.Base;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface IMemberRepository : IRepository<MemberDTO, int>
    {
        Task<IEnumerable<MemberDTO>> GetMemberListAsync();

        Task<bool> AddStudentsFromExcel(IEnumerable<MemberDTO> memberDTOs);

        Task<bool> EditMember(MemberDTO dto);

        Task<MemberDTO> GetMemberAsync(string account, string name);
    }
}