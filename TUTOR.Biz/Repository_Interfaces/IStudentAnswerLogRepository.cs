using System.Net;
using TUTOR.Biz.Domain.DTO;
using TUTOR.Biz.Models.Requests;
using TUTOR.Biz.Models;
using TUTOR.Biz.Repository_Interfaces.Base;
using X.PagedList;

namespace TUTOR.Biz.Repository_Interfaces
{
    public interface IStudentAnswerLogRepository : IRepository<StudentAnswerLogDTO, int>
    {
        Task<IEnumerable<StudentAnswerLogDTO>> GetStudentAnswerLogsAsync(int studentId);

        Task<bool> AddStudentAnswerLogAsync(StudentAnswerLogDTO answerLogDTO);

        Task<StudentAnswerLogDTO> GetLastStudentAnswerLogAsync(int studentId, string type);

        Task<StudentAnswerLogDTO> GetStudentAnswerLogAsync(int studentId, string type, string questionNum);
    }
}