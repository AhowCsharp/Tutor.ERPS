namespace TUTOR.Biz.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 台北時間(UTC+8)
        /// <para>避免伺服器時間與台北時間不同</para>
        /// </summary>
        public static DateTime TaipeiNow
        {
            get
            {
                return DateTime.UtcNow.AddHours(8);
            }
        }

        public static DateTime TaipeiToday
        {
            get
            {
                return DateTime.UtcNow.AddHours(8).Date;
            }
        }

        /// <summary>
        /// 運達特殊日期格式轉換 (預存程序用)
        /// </summary>
        /// <returns></returns>
        public static DateTime CustomerConvert(string value)
        {
            try
            {
                if (DateTime.TryParse(value, out DateTime _value))
                {
                    return _value;
                }
                else
                {
                    // maybe 20221110
                    var year = int.Parse(value.Substring(0, 4));
                    var month = int.Parse(value.Substring(4, 2));
                    var day = int.Parse(value.Substring(6, 2));

                    return new DateTime(year, month, day);
                }
            }
            catch
            {
                return new DateTime();
            }
        }
    }
}
