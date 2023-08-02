namespace  TUTOR.Biz.Domain.DTO
{
    public class TokenLogDTO
    {
        public string Token { get; set; }

        /// <summary>
        /// IP位置
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 請求網址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Token開始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Token結束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 紀錄時間
        /// </summary>
        public DateTime LogDate { get; set; }
    }
}
