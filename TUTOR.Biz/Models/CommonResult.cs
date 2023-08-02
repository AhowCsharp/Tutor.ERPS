namespace TUTOR.Biz.Models
{
    public class CommonResult
    {
        /// <summary>
        /// 來源ID
        /// </summary>
        public int RefId { get; set; }

        /// <summary>
        /// 結果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public IList<string> Msg { get; set; } = new List<string>();

        public string JsonData { get; set; }
    }
}