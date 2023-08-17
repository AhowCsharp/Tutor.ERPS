using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class SentenceManageDTO : IDTO
    {
        public int Id { get; set; }
        public int? QuestionTypeId { get; set; }
        public string QuestionSentence { get; set; }
        public string Mp3FileName { get; set; }
        public string Mp3FileUrl { get; set; }
        public string QuestionAnswer { get; set; }
        public int? StudyLevel { get; set; }
        public string QuestionSentenceChinese { get; set; }

        public string? TypeName { get; set; }
    }
}