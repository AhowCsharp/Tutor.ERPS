using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class GameWordsDTO : IDTO
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int? HardLevel { get; set; }
        public int? Status { get; set; }
        public string WordKind { get; set; }

        public string Mp3Url { get; set; }
    }
}