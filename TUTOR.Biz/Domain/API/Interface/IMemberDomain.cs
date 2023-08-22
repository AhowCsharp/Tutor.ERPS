using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models.Responses.Member;

namespace TUTOR.Biz.Domain.API.Interface
{
    public interface IMemberDomain
    {
        Task<bool> AddStudentsFromExcel(Stream excelStream);

        Task<MemberResponse> GetMemberListAsync();

        Task<bool> SaveStudentInfo(List<MemberRequest> request);

        Task<bool> DeleteStudentInfo(int studentId);

        Task<bool> AddStudentInfo(MemberRequest request);
    }
}