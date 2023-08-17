using Microsoft.AspNetCore.Http;
using TUTOR.Biz.Models;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models.Responses.StudentAnswerLog;

namespace TUTOR.Biz.Domain.API.Interface
{
    public interface IStudentAnswerLogDomain
    {
        Task<StudentAnswerLogResponse> GetStudentAnswerLogsAsync(int id);
    }
}