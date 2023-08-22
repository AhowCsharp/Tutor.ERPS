using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class SentenceTypeDTO : IDTO
    {
        public int id { get; set; }
        public string type { get; set; }
        public int? studyLevel { get; set; }
    }
}