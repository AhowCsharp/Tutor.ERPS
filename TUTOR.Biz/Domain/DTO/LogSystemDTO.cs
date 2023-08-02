using System;

namespace TUTOR.Biz.Domain.DTO
{
    public class LogSystemDTO
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public int? OfficialAccountId { get; set; }
        public string UserName { get; set; }
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string Url { get; set; }
        public bool? Https { get; set; }
        public string ServerAddress { get; set; }
        public string RemoteAddress { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
    }
}