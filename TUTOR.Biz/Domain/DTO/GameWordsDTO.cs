using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class GameWordsDTO : IDTO
    {
        public int id { get; set; }
        public string word { get; set; }
        public int? hardLevel { get; set; }
        public int? status { get; set; }
        public string wordKind { get; set; }

        public string mp3Url { get; set; }

        public string wordChinese { get; set; }
    }
}