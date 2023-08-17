using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class StudentAnswerLogDTO : IDTO
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public string QuestionType { get; set; }
        public string QuestionNumber { get; set; }
        public string AnswerLog { get; set; }
        public string CostTime { get; set; }
        public int? Status { get; set; }
        public DateTime? AnswerDate { get; set; }
    }
}