using TUTOR.Biz.SeedWork;

namespace TUTOR.Biz.Domain.DTO
{
    public class TokenDTO : IDTO
    {
        public string Token { get; set; }

        /// <summary>
        /// 允許IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}