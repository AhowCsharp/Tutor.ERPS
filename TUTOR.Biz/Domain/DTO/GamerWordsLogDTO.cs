using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public partial class GamerWordsLogDTO : IDTO
    {
        public int Id { get; set; }
        public string Words { get; set; }
        public int HardLevel { get; set; }
        public string Gamer { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}