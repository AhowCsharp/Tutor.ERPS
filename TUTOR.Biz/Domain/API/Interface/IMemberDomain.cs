using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models.Responses.Member;

namespace TUTOR.Biz.Domain.API.Interface
{
    public interface IMemberDomain
    {
        Task<CommonResult> AddStudentsFromExcel(Stream excelStream);

        Task<MemberResponse> GetMemberListAsync();

        Task<CommonResult> SaveStudentInfo(int studentId, MemberRequest request);

        Task<CommonResult> DeleteStudentInfo(int studentId);

        Task<CommonResult> AddStudentInfo(MemberRequest request);
    }
}