using System;

namespace TUTOR.Biz.Models
{
    public class CurrentUser
    {
        public string ApToken { get; set; }

        public int Id { get; set; }

        public Guid UUID { get; set; }

        public int OfficialAccountId { get; set; }

        public string Name { get; set; }

        public string LineUserId { get; set; }

        public string Email { get; set; }

        public int AuthType { get; set; }

        public bool SkipLineUserId { get; set; }
    }
}