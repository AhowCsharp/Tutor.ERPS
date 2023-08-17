using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class SentenceTypeDTO : IDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? StudyLevel { get; set; }
    }
}