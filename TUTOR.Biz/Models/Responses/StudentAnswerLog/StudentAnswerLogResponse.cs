using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.StudentAnswerLog
{
    public class StudentAnswerLogResponse
    {
        public StudentAnswerLogResponse(IEnumerable<StudentAnswerLogDTO> list)
        {
            List = list;
        }

        public IEnumerable<StudentAnswerLogDTO> List { get; set; }
    }
}