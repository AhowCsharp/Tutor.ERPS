using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class StudentAnswerLogDTO : IDTO
    {
        public int id { get; set; }
        public int? studentId { get; set; }
        public string questionType { get; set; }
        public string questionNumber { get; set; }
        public string answerLog { get; set; }
        public string costTime { get; set; }
        public int? status { get; set; }
        public DateTime? answerDate { get; set; }
    }
}