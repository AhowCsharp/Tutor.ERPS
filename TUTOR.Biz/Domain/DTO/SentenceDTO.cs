using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class SentenceDTO : IDTO
    {
        public int id { get; set; }
        public int? questionTypeId { get; set; }
        public string questionSentence { get; set; }
        public string mp3FileName { get; set; }
        public string mp3FileUrl { get; set; }
        public int? studyLevel { get; set; }
        public string questionSentenceChinese { get; set; }

        public bool? isPassing { get; set; }
        public string? typeName { get; set; }
        public int? questionNum { get; set; }
        public Subject? subject { get; set; }
    }

    public class Subject
    {
        public string mp3FileName { get; set; }
        public string questionSentence { get; set; }

        public string mp3FileUrl { get; set; }
        public int? questionNum { get; set; }
        public string? typeName { get; set; }
        public string? preAnswer { get; set; }
    }
}