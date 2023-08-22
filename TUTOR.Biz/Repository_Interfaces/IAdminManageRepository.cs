using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces.Base;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface IAdminManageRepository : IRepository<AdminManageDTO, int>
    {
        Task<AdminManageDTO?> GetAdminManageAsync(LoginRequest loginRequest);

        Task<MemberDTO?> GetMemberAsync(LoginRequest loginRequest);

    }
}