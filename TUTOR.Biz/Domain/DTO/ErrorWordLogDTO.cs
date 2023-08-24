using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public partial class ErrorWordLogDTO : IDTO
    {
        public int Id { get; set; }
        public string ErrorWord { get; set; }
        public string Gamer { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? HardLevel { get; set; }
    }
}